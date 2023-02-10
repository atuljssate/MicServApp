using MSA.web;
using MSA.web.Models;
using MSA.web.Services.IServices;

namespace MSA.web.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task<T> GetCouponAsync<T>(string couponCode, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
