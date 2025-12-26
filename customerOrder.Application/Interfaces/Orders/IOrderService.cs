using CustomerOrder.Application.DTOs.Order;

namespace CustomerOrder.Application.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderDto order);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> UpdateOrderAsync(OrderDto order, int orderId);
    }
}
//Orders must have a total amount greater than zero.
// Orders must not be created or updated for non-existing customers. 

