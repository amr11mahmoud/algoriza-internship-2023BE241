using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Service.Dtos.Response.Bookings;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Service.Dtos.Response.Patients;

namespace Vezeeta.Service.Helpers
{
    public static class BookingHelper
    {
        public static IEnumerable<GetDoctorBookingDto> MapDoctorBookingsResultToResponseDto(IEnumerable<Booking> bookings)
        {
            var response = new List<GetDoctorBookingDto>();

            bookings.ToList().ForEach(booking =>
            {
                var bookingDto = new GetDoctorBookingDto
                {
                    Status = booking.Status.ToString(),
                    Age = UserHelper.CalculateUserAge(booking.Patient.DateOfBirth),
                    Email = booking.Patient.Email,
                    Gender = booking.Patient.Gender.ToString(),
                    ImageUrl = booking.Patient.ImageUrl,
                    PatientName = booking.Patient.FullName,
                    PhoneNumber = booking.Patient.PhoneNumber,
                    Appointment = new GetBookingAppointmentDto
                    {
                        Day = booking.Time.Appointment.Day.ToString(),
                        Time = booking.Time.Time
                    }
                };

                response.Add(bookingDto);
            });

            return response;
        }

        public static IEnumerable<GetPatientBookingDto> MaPatientBookingsResultToResponseDto(IEnumerable<Booking> bookings)
        {
            var response = new List<GetPatientBookingDto>();

            bookings.ToList().ForEach(booking =>
            {
                var bookingDto = new GetPatientBookingDto
                {
                    Status = booking.Status.ToString(),
                    ImageUrl = booking.Doctor.ImageUrl,
                    DoctorName = booking.Doctor.FullName,
                    Day = booking.Time.Appointment.Day.ToString(),
                    DiscountCode = booking.Coupon != null ? booking.Coupon.Code: null,
                    FinalPrice = booking.FinalPrice,
                    Price = booking.Price,
                    Time = booking.Time.Time,
                    Specialization = booking.Doctor.Specialization.Name
                };

                response.Add(bookingDto);
            });

            return response;
        }
    }
}
