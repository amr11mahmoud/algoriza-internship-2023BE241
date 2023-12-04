using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Domain.Bookings
{
    public class Booking : Entity<int>
    {
        public RequestStatus Status { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual User Patient { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual User Doctor { get; set; }

        [ForeignKey("Coupon")]
        public int? CouponId { get; set; }
        public virtual Coupon? Coupon { get; set; }

        [ForeignKey("AppointmentTime")]
        public int AppointmentTimeId { get; set; }
        public virtual AppointmentTime AppointmentTime { get; set; }
    }
}
