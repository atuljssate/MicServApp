using MSA.Services.OrderAPI.Models;

namespace MSA.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task <bool> AddOrder(OrderHeader orderHeader);
        //Task DeleteOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
