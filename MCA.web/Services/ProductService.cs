using MSA.web;
using MSA.web.Models;
using MSA.web.Services.IServices;

namespace MSA.web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductAsync<T>(string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }
    }
}
