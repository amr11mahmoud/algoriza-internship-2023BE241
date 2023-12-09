using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service.Bookings;
using Vezeeta.Core.Service.Settings;
using Vezeeta.Core.Shared;
using Vezeeta.Repository.Repositories;

namespace Vezeeta.Service.Settings
{
    public class CouponService : ICouponService
    {
        private readonly IBaseRepository<Coupon> _couponRepository;
        private readonly IBookingRepository _bookingRepository;

        public CouponService(IBaseRepository<Coupon> couponRepository, IBookingRepository bookingRepository)
        {
            _couponRepository = couponRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Coupon>> GetAllCouponsAsync()
        {
            IEnumerable<Coupon> coupons = await _couponRepository.GetAllAsync();

            return coupons;
        }

        public async Task<Coupon?> GetCouponAsync(int id)
        {
            Coupon? coupon = await _couponRepository.GetByIdAsync(id);
            return coupon;
        }

        public async Task<Result<bool>> AddCouponAsync(Coupon coupon)
        {
            bool couponAlreadyExist = await _couponRepository.AnyAsync(c => c.Code == coupon.Code);
            if (couponAlreadyExist) return Result.Failure<bool>(Error.Errors.Settings.CouponCodeAlreadyExist(coupon.Code));

            await _couponRepository.AddAsync(coupon);
            await _couponRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Result<bool>> DeactivateCouponAsync(int id)
        {
            Coupon? coupon = await _couponRepository.GetByIdAsync(id);
            if (coupon == null) return Result.Failure<bool>(Error.Errors.Settings.CouponNotFound());

            coupon.Active = false;

            _couponRepository.Update(coupon);
            await _couponRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteCouponAsync(int id)
        {
            bool deletedSuccessfully = await _couponRepository.DeleteAsync(id);
            if (!deletedSuccessfully) return Result.Failure<bool>(Error.Errors.Settings.CouponNotFound());

            await _couponRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Result<bool>> EditCouponAsync(Coupon newCouponData)
        {
            Coupon? coupon = await _couponRepository.GetByIdAsync(newCouponData.Id);
            if (coupon == null) return Result.Failure<bool>(Error.Errors.Settings.CouponNotFound());

            coupon.Update(newCouponData);

            _couponRepository.Update(coupon);
            await _couponRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Coupon?> GetCouponAsync(string code)
        {
            Coupon? coupon = await _couponRepository.FindAsync(c=> c.Code == code);
            return coupon;
        }

        public async Task<Result<bool>> CouponIsActive(string code)
        {
            Coupon? coupon = await GetCouponAsync(code);
            if (coupon == null) return Result.Failure<bool>(Error.Errors.Settings.CouponNotFound());

            return Result.Success(coupon.Active);
        }

        public async Task<Result<bool>> CouponIsUsed(string code)
        {
            Coupon? coupon = await GetCouponAsync(code);
            if (coupon == null) return Result.Failure<bool>(Error.Errors.Settings.CouponNotFound());

            bool isUsed = await _bookingRepository.AnyAsync(booking=> booking.CouponId == coupon.Id && booking.Status != Core.Enums.RequestStatus.Canceled);

            return Result.Success(isUsed);
        }

        public float CalculateFinalPrice(Coupon coupon, float price)
        {
            if (coupon.DiscountType == DiscountType.Value) return price - coupon.Value;

            float discount = price * ((coupon.Value) / 100);

            return price - discount;
        }
    }
}
