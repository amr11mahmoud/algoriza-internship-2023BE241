using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Core.Domain.Base;

namespace Vezeeta.Core.Domain.Appointments
{
    public class AppointmentTime : Entity<int>
    {

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public DateTime Time { get; set; }
        public bool Booked { get; set; }
        public Appointment Appointment { get; set; }
    }
}
