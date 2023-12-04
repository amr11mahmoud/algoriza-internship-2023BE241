using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Domain.Appointments
{
    public class Appointment : Entity<int>
    {
        public Days Day { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public User Doctor { get; set; }
        public virtual IEnumerable<AppointmentTime> Times { get; set; }
    }
}
