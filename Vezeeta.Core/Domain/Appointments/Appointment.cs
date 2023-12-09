using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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

        public override string ToString()
        {
           List<string> times = Times.Select(t => t.Time).ToList();

            StringBuilder dayTimes = new StringBuilder();

            times.ForEach(t =>
            {
                dayTimes.Append(t);
            });

            return Day.ToString() + " - " +dayTimes.ToString();
        }
    }
}
