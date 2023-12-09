using System.Text.Json.Serialization;

namespace Vezeeta.Service.Dtos.Response.Appointments
{
    public class GetAppointmentDto
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public IEnumerable<GetAppointmentTimeDto> Times { get; set; }
    }

    public class GetAppointmentTimeDto
    {
        public int Id { get; set; }
        public string Time { get; set; }
    }
}
