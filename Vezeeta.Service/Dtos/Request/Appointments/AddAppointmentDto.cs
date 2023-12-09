using System.Text.Json.Serialization;
using Vezeeta.Core.Enums;

namespace Vezeeta.Service.Dtos.Request.Appointments
{
    public class AddAppointmentDto
    {
        public string Day { get; set; }
        public IEnumerable<AddAppointmentTimeDto> Times { get; set; }

        // internal usage
        [JsonIgnore]
        public int DoctorId { get; set; }
    }

    public class AddAppointmentTimeDto
    {
        public string Time { get; set; }
    }
}
