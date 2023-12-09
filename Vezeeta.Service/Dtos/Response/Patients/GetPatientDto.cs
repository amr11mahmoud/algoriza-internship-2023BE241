using System.Text.Json.Serialization;
using Vezeeta.Service.Dtos.Response.Base;

namespace Vezeeta.Service.Dtos.Response.Patients
{
    public class GetPatientDto : GetUserDto
    {
        [JsonIgnore]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirthString => DateOfBirth.Date.ToString("dd/MM/yyyy");
        public string Email { get; set; }
    }
}
