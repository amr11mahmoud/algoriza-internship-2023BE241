using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Settings
{
    public interface ICouponService
    {
        Task<Coupon?> GetCouponAsync(int id);
        Task<Coupon?> GetCouponAsync(string code);
        Task<IEnumerable<Coupon>> GetAllCouponsAsync();
        Task<Result<bool>> AddCouponAsync(Coupon coupon);
        Task<Result<bool>> EditCouponAsync(Coupon coupon);
        Task<Result<bool>> DeleteCouponAsync(int id);
        Task<Result<bool>> DeactivateCouponAsync(int id);
        Task<Result<bool>> CouponIsActive(string code);
        Task<Result<bool>> CouponIsUsed(string code);
        float CalculateFinalPrice(Coupon coupon, float price);
    }
}
