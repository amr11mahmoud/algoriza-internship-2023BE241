using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Enums;
using Vezeeta.Service.Dtos.Response.Appointments;

namespace Vezeeta.Service.Dtos.Response.Bookings
{
    public class GetBookingAppointmentDto
    {
        public string Day { get; set; }
        public string Time { get; set; }
    }
}
