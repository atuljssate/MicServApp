using MSA.web.Models;
using MSA.web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MSA.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var accessToken = await HttpContext.GetTokenAsync("id_token");
            var response = await _productService.GetAllProductAsync<ResponseDto>(accessToken);
            if (response != null && response.Success)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("id_token");
                var response = await _productService.CreateProductAsync<ResponseDto>(product, accessToken);
                if (response != null && response.Success)
                {
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(product);
        }
        public async Task<IActionResult> ProductEdit( int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("id_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.Success)
            {
                ProductDto product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("id_token");
                var response = await _productService.CreateProductAsync<ResponseDto>(product, accessToken);
                if (response != null && response.Success)
                {
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(product);
        }
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("id_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.Success)
            {
                ProductDto product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("id_token");
                var response = await _productService.DeleteProductAsync<ResponseDto>(product.ProductId, accessToken);
                if (response != null && response.Success)
                {
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(product);
        }
    }
}

