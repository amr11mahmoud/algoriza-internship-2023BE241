using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Domain.Bookings
{
    public class Booking:Entity<Guid>
    {
        public RequestStatus Status { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public User Patient { get; set; }

        [ForeignKey("Doctor")]
        public Guid DoctorId { get; set; }
        public User Doctor { get; set; }

        [ForeignKey("Coupon")]
        public int? CouponId { get; set; }
        public Coupon? Coupon { get; set; }

        [ForeignKey("AppointmentSchedule")]
        public int AppointmentScheduleId { get; set; }
        public AppointmentSchedule AppointmentSchedule { get; set; } 
    }
}
