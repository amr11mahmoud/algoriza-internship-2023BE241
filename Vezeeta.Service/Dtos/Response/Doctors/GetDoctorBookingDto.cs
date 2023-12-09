using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Response.Appointments;
using Vezeeta.Service.Dtos.Response.Bookings;

namespace Vezeeta.Service.Dtos.Response.Doctors
{
    public class GetDoctorBookingDto
    {
        public string Status { get; set; }
        public string PatientName { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public GetBookingAppointmentDto Appointment { get; set; }
    }
}
