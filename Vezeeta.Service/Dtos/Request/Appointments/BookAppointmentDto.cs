using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Service.Dtos.Request.Appointments
{
    public class BookAppointmentDto
    {
        public int TimeId { get; set; }
        public string? CouponCode { get; set; }
    }
}
