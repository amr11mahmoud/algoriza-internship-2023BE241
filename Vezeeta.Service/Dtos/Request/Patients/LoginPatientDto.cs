﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Service.Dtos.Request.Patients
{
    public class LoginPatientDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}