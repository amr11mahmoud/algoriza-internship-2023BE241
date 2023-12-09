using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Dtos.Response.Doctors;

namespace Vezeeta.Service.Dtos.Response.Patients
{
    public class GetPatientAndRequestsDto
    {
        public GetPatientDto Details { get; set; }
        public IEnumerable<PatientRequestDto> Requests { get; set; }
    }

    public class PatientRequestDto
    {
        public string? ImageUrl { get; set; }
        public string DoctorName { get; set; }
        public SpecializationDto Specialization { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public float Price { get; set; }
        public float FinalPrice { get; set; }
        public string DiscountCode { get; set; }
        public string Status { get; set; }
    }
}
