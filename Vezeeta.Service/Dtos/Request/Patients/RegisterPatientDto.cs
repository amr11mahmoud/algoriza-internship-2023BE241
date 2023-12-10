using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Enums;
using Vezeeta.Service.Dtos.Request.Base;

namespace Vezeeta.Service.Dtos.Request.Patients
{
    public class RegisterPatientDto:AddUserDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
