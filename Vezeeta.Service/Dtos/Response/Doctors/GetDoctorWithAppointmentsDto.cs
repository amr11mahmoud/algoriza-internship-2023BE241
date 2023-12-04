using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Dtos.Response.Appointments;

namespace Vezeeta.Service.Dtos.Response.Doctors
{
    public class GetDoctorWithAppointmentsDto: GetDoctorDto
    {
        public IEnumerable<GetAppointmentDto> Appointments { get; set; }
    }
}
