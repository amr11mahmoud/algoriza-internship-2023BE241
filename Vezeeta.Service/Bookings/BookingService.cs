using System.Linq.Expressions;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service.Appointments;
using Vezeeta.Core.Service.Bookings;
using Vezeeta.Core.Service.Settings;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Extensions;

namespace Vezeeta.Service.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAppointmentService _appointmentService;
        private readonly ICouponService _couponService;
        private readonly IDoctorRepository _doctorRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IAppointmentService appointmentService,
            ICouponService couponService,
            IDoctorRepository doctorRepository)
        {
            _bookingRepository = bookingRepository;
            _appointmentService = appointmentService;
            _couponService = couponService;
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(
            int discriminatorId,
            UserDiscriminator discriminator,
            int page,
            int pageSize,
            string? day = null,
            string[]? includes = null)
        {

            List<Expression<Func<Booking, bool>>> conditions = new List<Expression<Func<Booking, bool>>>();

            if (discriminator == UserDiscriminator.Patient) conditions.Add((b) => b.PatientId == discriminatorId);
            if (discriminator == UserDiscriminator.Doctor) conditions.Add((b) => b.DoctorId == discriminatorId);
            if (!string.IsNullOrEmpty(day)) conditions.Add((b) => b.Time.Appointment.Day == Enum.Parse<Days>(day));

            var searchCondition = conditions.Aggregate((x, y) => x.And(y));

            IEnumerable<Booking> bookings = await _bookingRepository.FindAllAsync(searchCondition, page, pageSize, includes);

            return bookings;
        }

        public async Task<Result<bool>> BookAsync(int timeId, int patientId, string? couponCode = null)
        {
            AppointmentTime? time = await _appointmentService.GetTimeAsync(timeId, new[] { AppConsts.DomainModels.AppointmentDoctor });
            Coupon? coupon = null;

            Result<bool> validateTimeResult = ValidateTimeBooking(time);
            if (validateTimeResult.IsFailure) return Result.Failure<bool>(validateTimeResult.Error);

            if (!string.IsNullOrEmpty(couponCode)) coupon = await _couponService.GetCouponAsync(couponCode);

            if (coupon != null)
            {
                Result<bool> validateCouponResult = await ValidateBookingCoupon(coupon, patientId);

                if (validateCouponResult.IsFailure) return Result.Failure<bool>(validateCouponResult.Error);
            }

            float finalPrice = GetFinalPrice(time.Appointment.Doctor.Price, coupon);

            await StoreBooking(time, coupon, patientId, finalPrice);

            return Result.Success(true);
        }

        private float GetFinalPrice(float basePrice, Coupon? coupon)
        {
            float finalPrice = basePrice;

            if (coupon != null) finalPrice = _couponService.CalculateFinalPrice(coupon, basePrice);

            return finalPrice;
        }

        public async Task<bool> StoreBooking(AppointmentTime time, Coupon coupon, int patientId, float finalPrice)
        {
            Booking booking = new Booking
            {
                PatientId = patientId,
                Price = time.Appointment.Doctor.Price,
                FinalPrice = finalPrice,
                Time = time,
                Coupon = coupon,
                Doctor = time.Appointment.Doctor
            };

            booking.Book();

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool>> ConfirmCheckUp(int bookingId, int doctorId)
        {
            Booking? booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null) return Result.Failure<bool>(Error.Errors.Bookings.BookingNotFound());
            if (booking.Status != RequestStatus.Pending) return Result.Failure<bool>(Error.Errors.Bookings.BookingStatusIsNotPending());
            if (booking.DoctorId != doctorId) return Result.Failure<bool>(Error.Errors.Bookings.InvalidBookingDoctor());

            booking.ConfirmCheckUp();

            _bookingRepository.Update(booking);

            await _bookingRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Result<bool>> CancelBookingAsync(int bookingId, int patientId)
        {
            Booking? booking =
                await _bookingRepository.FindAsync(b => b.Id == bookingId && b.PatientId == patientId, includes: new[] { AppConsts.DomainModels.AppointmentTime });

            if (booking == null) return Result.Failure<bool>(Error.Errors.Bookings.BookingNotFound());
            if (booking.Status != RequestStatus.Pending) return Result.Failure<bool>(Error.Errors.Bookings.BookingStatusIsNotPending());

            booking.CancelBooking();

            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<List<int>> GetBookingsCountAsync()
        {
            var bookingsCount = await _bookingRepository.CountBookingRequestsAsync();

            var countOfCompleted = bookingsCount.FirstOrDefault(r => r.Status == AppConsts.General.Completed);
            var countOfPending = bookingsCount.FirstOrDefault(r => r.Status == AppConsts.General.Pending);
            var countOfCanceled = bookingsCount.FirstOrDefault(r => r.Status == AppConsts.General.Canceled);
            var countOfTotal = bookingsCount.FirstOrDefault(r => r.Status == AppConsts.General.Total);

            var result = new List<int>
            {
                countOfCompleted != null ? countOfCompleted.Count : 0,
                countOfPending != null ? countOfPending.Count : 0,
                countOfCanceled != null ? countOfCanceled.Count : 0,
                countOfTotal != null ? countOfTotal.Count : 0,
            };

            return result;
        }

        public async Task<int> GetBookingsCountAsync(int patientId)
        {
            var bookingsCount = await _bookingRepository.CountAsync(b => b.PatientId == patientId);
            return bookingsCount;
        }

        private Result<bool> ValidateTimeBooking(AppointmentTime? time)
        {
            if (time == null) return Result.Failure<bool>(Error.Errors.Appointments.AppointmentTimeNotFound());

            if (_appointmentService.IsTimeBooked(time)) return Result.Failure<bool>(Error.Errors.Appointments.AppointmentIsBooked());

            return Result.Success(true);
        }

        private async Task<Result<bool>> ValidateBookingCoupon(Coupon? coupon, int patientId)
        {
            if (coupon == null) return Result.Failure<bool>(Error.Errors.Settings.CouponNotFound());
            if (!coupon.Active) return Result.Failure<bool>(Error.Errors.Settings.CouponNotActive());

            int patientBookingsCount = await GetBookingsCountAsync(patientId);
            if (patientBookingsCount < coupon.NumberOfRequests)
                return Result.Failure<bool>(Error.Errors.Settings.CouponNotApplicable(patientBookingsCount, coupon.NumberOfRequests));

            bool usedBefore = await _bookingRepository.AnyAsync(b => b.PatientId == patientId && b.CouponId == coupon.Id && b.Status != RequestStatus.Canceled);
            if (usedBefore) return Result.Failure<bool>(Error.Errors.Settings.CouponAlreadyUsed());

            return Result.Success(true);
        }
    }
}
