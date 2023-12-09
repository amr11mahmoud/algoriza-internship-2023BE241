using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Dtos.Response.Bookings;

namespace Vezeeta.Service.Dtos.Response.Patients
{
    public class GetPatientBookingDto
    {
        public string DoctorName { get; set; }
        public string ImageUrl { get; set; }
        public string? DiscountCode { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public float FinalPrice { get; set; }
        public string Specialization { get; set; }
    }
}
