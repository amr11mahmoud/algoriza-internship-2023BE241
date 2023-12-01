using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Domain.Appointments
{
    public class Appointment : Entity<int>
    {
        public Days Day { get; set; }

        [ForeignKey("Doctor")]
        public Guid DoctorId { get; set; }
        public User Doctor { get; set; }
        public virtual IEnumerable<AppointmentSchedule> AppointmentSchedules { get; set; }
    }
}
