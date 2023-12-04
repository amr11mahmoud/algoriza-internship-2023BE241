using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Enums;
using Vezeeta.Service.Dtos.Request.Base;

namespace Vezeeta.Service.Dtos.Request.Patients
{
    public class RegisterPatientDto:AddUserDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
