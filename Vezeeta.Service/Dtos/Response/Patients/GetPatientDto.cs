using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Dtos.Response.Base;

namespace Vezeeta.Service.Dtos.Response.Patients
{
    public class GetPatientDto: GetUserDto
    {
        public string Email { get; set; }
    }
}
