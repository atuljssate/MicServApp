using MSA.Services.CouponAPI.Models.Dto;

namespace MSA.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
