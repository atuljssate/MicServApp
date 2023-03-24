using MSA.Services.ShoppingCartAPI.Models.Dto;
using Newtonsoft.Json;

namespace MSA.Services.ShoppingCartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient httpClient;

        public CouponRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;   
        }

        public async Task<CouponDto> GetCoupon(string couponName)
        {
            var response= await httpClient.GetAsync($"/api/coupon/{couponName}");
            var apiContent= await response.Content.ReadAsStringAsync();
            var resp= JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.Success)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();            
        }
    }
}
