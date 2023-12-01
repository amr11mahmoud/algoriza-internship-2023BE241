using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Domain.Coupons
{
    public class Coupon:Entity<int>
    {
        public string Code { get; set; }
        public bool Active { get; set; }
        public DiscountType DiscountType { get; set; }
        public float Value { get; set; }
        public byte NumberOfRequests { get; set; }

    }
}
