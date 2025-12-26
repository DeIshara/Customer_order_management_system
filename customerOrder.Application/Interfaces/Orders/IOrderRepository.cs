using CustomerOrder.Domain.Entities;

namespace CustomerOrder.Application.Interfaces.Orders
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> UpdateOrderAsync(Order order, int orderId);
    }
}
