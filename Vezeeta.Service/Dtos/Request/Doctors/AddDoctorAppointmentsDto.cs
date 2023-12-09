using Vezeeta.Service.Dtos.Request.Appointments;

namespace Vezeeta.Service.Dtos.Request.Doctors
{
    public class AddDoctorAppointmentsDto
    {
        public float Price { get; set; }
        public IEnumerable<AddAppointmentDto> Appointments { get; set; }
    }
}
