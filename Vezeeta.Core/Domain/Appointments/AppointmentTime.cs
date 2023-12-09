using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Core.Domain.Base;

namespace Vezeeta.Core.Domain.Appointments
{
    public class AppointmentTime : Entity<int>
    {

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public string Time { get; set; }
        public bool Booked { get; private set; } = false;
        public Appointment Appointment { get; set; }

        public void UpdateTime(string time)
        {
            Time = time;
        }

        public void Book()
        {
            Booked = true;
        }

        public void CancelBooking()
        {
            Booked = false;
        }
    }
}
