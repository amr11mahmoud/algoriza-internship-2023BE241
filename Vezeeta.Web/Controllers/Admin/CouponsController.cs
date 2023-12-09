using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Service.Settings;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Coupons;
using Vezeeta.Service.Dtos.Response.Coupons;

namespace Vezeeta.Web.Controllers.Admin
{
    public class CouponsController : SettingsController
    {
        private readonly IMapper _mapper;
        private readonly ICouponService _couponService;

        public CouponsController(IMapper mapper, ICouponService couponService)
        {
            _mapper = mapper;
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<ActionResult<GetCouponDto>> Get(int id)
        {
            Coupon? coupon = await _couponService.GetCouponAsync(id);
            if (coupon == null) return BadRequest(Error.Errors.Settings.CouponNotFound());

            return Ok(_mapper.Map<GetCouponDto>(coupon));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCouponDto>>> GetAll()
        {
            IEnumerable<Coupon> coupons = await _couponService.GetAllCouponsAsync();
            return Ok(_mapper.Map<IEnumerable<GetCouponDto>>(coupons));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add(AddCouponDto request)
        {
            Coupon coupon = _mapper.Map<Coupon>(request);

            Result<bool> addCouponResult = await _couponService.AddCouponAsync(coupon);

            if (addCouponResult.IsFailure) return BadRequest(addCouponResult.Error);

            return Ok(true);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Edit(EditCouponDto request)
        {
            Coupon coupon = _mapper.Map<Coupon>(request);

            Result<bool> editCouponResult = await _couponService.EditCouponAsync(coupon);

            if (editCouponResult.IsFailure) return BadRequest(editCouponResult.Error);

            return Ok(true);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            Result<bool> deleteCouponResult = await _couponService.DeleteCouponAsync(id);

            if (deleteCouponResult.IsFailure) return BadRequest(deleteCouponResult.Error);

            return Ok(true);
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Deactivate(int id)
        {
            Result<bool> deactivateCouponResult = await _couponService.DeactivateCouponAsync(id);

            if (deactivateCouponResult.IsFailure) return BadRequest(deactivateCouponResult.Error);

            return Ok(true);
        }
    }
}
