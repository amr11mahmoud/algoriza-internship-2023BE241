using System.ComponentModel.DataAnnotations;
using Vezeeta.Service.Dtos.Request.Base;

namespace Vezeeta.Service.Dtos.Request.Doctors
{
    public class AddDoctorDto : AddUserDto
    {
        [Required]
        public int SpecializationId { get; set; }
    }
}
