using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSA.web.Models;
using MSA.web.Services.IServices;
using Newtonsoft.Json;

namespace MSA.web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [Authorize]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var CartResp = await _cartService.ApplyCouponAsync<ResponseDto>(cartDto, accessToken);

            if (CartResp != null && CartResp.Success)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var CartResp = await _cartService.RemoveCouponAsync<ResponseDto>(cartDto.CartHeader.UserId, accessToken);

            if (CartResp != null && CartResp.Success)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Remove(int CartDetailsId)
        {
            var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var CartResp = await _cartService.RemoveFromCartAsync<ResponseDto>(CartDetailsId, accessToken);

            if (CartResp != null && CartResp.Success)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]        
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _cartService.Checkout<ResponseDto>(cartDto.CartHeader, accessToken);
                return RedirectToAction(nameof(Confirmation));
            }
            catch(Exception e) {
                return View(cartDto);
            }
        }
                
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }


        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var CartResp = await _cartService.GetCartByUserIdAsync<ResponseDto>(UserId, accessToken);

            CartDto cartDto = new();
            if (CartResp != null && CartResp.Success)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(CartResp.Result));
            }

            if (cartDto.CartHeader != null)
            {
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCouponAsync<ResponseDto>(cartDto.CartHeader.CouponCode, accessToken);
                    if (coupon != null && coupon.Success)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(coupon.Result));
                        cartDto.CartHeader.DiscountTotal = couponObj.DiscountAmount;
                    }
                }
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }

                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
            }

            return cartDto;
        }
    }
}
