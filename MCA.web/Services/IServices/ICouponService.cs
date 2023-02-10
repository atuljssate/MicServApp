using MSA.web.Models;

namespace MSA.web.Services.IServices
{
    public interface ICouponService
    {
        Task<T> GetCouponAsync<T>(string couponCode, string token = null);
    }
}
