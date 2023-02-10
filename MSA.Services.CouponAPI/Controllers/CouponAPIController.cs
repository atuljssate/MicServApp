using Microsoft.AspNetCore.Mvc;
using MSA.Services.CouponAPI.Models.Dto;
using MSA.Services.CouponAPI.Repository;

namespace MSA.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponAPIController : Controller
    {
        private ICouponRepository _couponRepository;
        protected ResponseDto _responseDto;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            this._responseDto = new ResponseDto();
        }

        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code)
        {
            try
            {
                var coupon = await _couponRepository.GetCouponByCode(code);
                _responseDto.Result = coupon;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }
    }
}
