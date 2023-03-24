using MSA.Services.ShoppingCartAPI.Models.Dto;

namespace MSA.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
