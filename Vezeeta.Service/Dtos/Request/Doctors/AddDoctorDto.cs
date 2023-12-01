using Microsoft.AspNetCore.Http;
using Vezeeta.Core.Domain.Lookup;

namespace Vezeeta.Service.Dtos.Request.Doctors
{
    public class AddDoctorDto
    {
        public IFormFile Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Specialization Specialize { get; set; }
        public int SpecializeId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
