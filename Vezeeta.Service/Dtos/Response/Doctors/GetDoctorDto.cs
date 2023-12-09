using Vezeeta.Service.Dtos.Response.Base;

namespace Vezeeta.Service.Dtos.Response.Doctors
{
    public class GetDoctorDto : GetUserDto
    {
        public SpecializationDto Specialization { get; set; }
        public float Price { get; set; }
    }

    public class SpecializationDto
    {
        public string Name { get; set; }
    }
}
