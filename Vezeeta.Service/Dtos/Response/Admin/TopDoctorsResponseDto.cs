﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Service.Dtos.Response.Admin
{
    public class TopDoctorsResponseDto
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Specialize { get; set; }
        public TopRequestsResponseDto Requests { get; set; }
    }
}
