using Vezeeta.Service.Dtos.Request.Base;

namespace Vezeeta.Service.Dtos.Request.Doctors
{
    public class AddDoctorDto : AddUserDto
    {
        public int SpecializationId { get; set; }
    }
}
