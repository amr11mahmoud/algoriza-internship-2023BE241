using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Dtos.Response.Doctors;

namespace Vezeeta.Service.Dtos.Response.Admin
{
    public class TopDoctorsResponseDto
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int Requests { get; set; }
    }
}
