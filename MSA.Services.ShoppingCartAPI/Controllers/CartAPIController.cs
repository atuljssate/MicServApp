using MCA.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using MSA.Services.ShoppingCartAPI.Messages;
using MSA.Services.ShoppingCartAPI.Models.Dto;
using MSA.Services.ShoppingCartAPI.Repository;

namespace MSA.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private ICartRepository _cartRepository;
        protected ResponseDto _responseDto;

        public CartAPIController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._responseDto = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserID(userId);
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cartDt;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cartDt;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool isSuccess = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }
        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Errors = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }
        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDto checkoutHeaderDto)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserID(checkoutHeaderDto.UserId);
                if (cartDto == null)
                {
                    return BadRequest();
                }
                checkoutHeaderDto.CartDetails = cartDto.CartDetails;
                //logic to add message to process order.

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
