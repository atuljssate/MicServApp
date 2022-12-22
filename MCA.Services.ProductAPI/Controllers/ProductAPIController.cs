using MCA.Services.ProductAPI.Models.Dto;
using MCA.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MCA.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepositorty;


        public ProductAPIController(IProductRepository productRepositorty)
        {
            _productRepositorty = productRepositorty;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepositorty.GetProductsAsync();
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDtos = await _productRepositorty.GetProductByIdAsync(id);
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]        
        public async Task<object> post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepositorty.CreateUpdateProductAsync(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepositorty.CreateUpdateProductAsync(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                Boolean IsOK = await _productRepositorty.DeleteProductByIdAsync(id);
                _response.Result = IsOK;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
