using MSA.Services.ShoppingCartAPI.Models.Dto;

namespace MSA.Services.ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task <CartDto>GetCartByUserID(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ApplyCoupon(string userId,string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
