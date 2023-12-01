using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Base;

namespace Vezeeta.Core.Domain.Appointments
{
    public class AppointmentSchedule:Entity<int>
    {
        public int AppointmentId { get; set; }

    }
}
