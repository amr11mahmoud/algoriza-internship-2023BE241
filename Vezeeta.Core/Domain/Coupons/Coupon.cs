using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Domain.Coupons
{
    public class Coupon : Entity<int>
    {
        public string Code { get; set; }
        public bool Active { get; set; } = true;
        public DiscountType DiscountType { get; set; }
        public float Value { get; set; }
        public byte NumberOfRequests { get; set; }

        public void Update(Coupon newData)
        {
            Value = newData.Value;
            Code = newData.Code;
            DiscountType = newData.DiscountType;
            NumberOfRequests = newData.NumberOfRequests;
        }
    }
}
