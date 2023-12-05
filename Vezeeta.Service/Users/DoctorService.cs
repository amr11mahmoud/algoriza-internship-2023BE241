using System.Linq.Expressions;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Service.Users
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserService _userService;

        public DoctorService(IDoctorRepository doctorRepository, IUserService userService, IBookingRepository bookingRepository)
        {
            _doctorRepository = doctorRepository;
            _userService = userService;
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<IEnumerable<User>>> GetAllDoctorsAsync(int page, int pageSize, string search, string[]? includes = null)
        {
            Expression<Func<User, bool>> searchCondition = (u) => u.Discriminator == UserDiscriminator.Doctor;

            if (!string.IsNullOrEmpty(search)) searchCondition = (u) => u.Discriminator == UserDiscriminator.Doctor && u.FullName.Contains(search);

            IEnumerable<User> doctors = await _doctorRepository.FindAllAsync(searchCondition, page, pageSize, includes);

            return Result.Success(doctors);
        }

        public async Task<Result<User>> GetDoctorAsync(int id, string[]? includes = null)
        {
            User? doctor = await _doctorRepository.FindAsync(d => d.Id == id && d.Discriminator == UserDiscriminator.Doctor, includes);

            if (doctor == null)
            {
                return Result.Failure<User>(Error.Errors.Doctors.DoctorNotFound());
            }

            return Result.Success(doctor);
        }

        public async Task<Result<int>> GetDoctorsCountAsync()
        {
            var doctorsCount = await _doctorRepository.CountAsync(u => u.Discriminator == UserDiscriminator.Doctor);

            return Result.Success(doctorsCount);
        }

        public async Task<Result<bool>> AddDoctorAsync(User user)
        {

            Result<User> registerDoctorResult =
                await _userService.RegisterUserAsync(user, AppConsts.User.DoctorBasePassword, UserDiscriminator.Doctor, new[] { AppConsts.Roles.Doctor });

            if (registerDoctorResult.IsFailure) return Result.Failure<bool>(registerDoctorResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<bool>> UpdateDoctorAsync(User user)
        {
            var updateDoctorResult = await _userService.UpdateUserAsync(user);

            if (updateDoctorResult.IsFailure) return Result.Failure<bool>(updateDoctorResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteDoctorAsync(int id)
        {
            bool doctorHasRequests = await _bookingRepository.AnyAsync(b => b.DoctorId == id);

            if (doctorHasRequests)
            {
                return Result.Failure<bool>(Error.Errors.Doctors.DoctorHasRequests());
            }

            var deleteDoctorResult = await _userService.DeleteUserAsync(id);

            if (deleteDoctorResult.IsFailure) return Result.Failure<bool>(deleteDoctorResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<IEnumerable<Booking>>> GetDoctorBookings(int doctorId, int page, int pageSize, DateTime? date, string[]? includes = null)
        {
            Expression<Func<Booking, bool>> searchCondition = (b) => b.DoctorId == doctorId;

            if (date != null) searchCondition = (b) => b.DoctorId == doctorId && b.Date == date;

            IEnumerable<Booking> bookings = await _bookingRepository.FindAllAsync(searchCondition, page, pageSize, includes);

            return Result.Success(bookings);
        }
    }
}
