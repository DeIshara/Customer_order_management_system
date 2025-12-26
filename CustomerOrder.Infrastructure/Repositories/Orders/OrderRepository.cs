using CustomerOrder.Application.Interfaces.Orders;
using CustomerOrder.Domain.Entities;

namespace CustomerOrder.Infrastructure.Repositories.Orders
{
    public class OrderRepository: IOrderRepository
    {
        public OrderRepository() { }

        List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 250.00m },
            new Order { OrderId = 2, CustomerId = 2, OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 150.00m },
            new Order { OrderId = 3, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 300.00m }
        };
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var customerOrders = orders.Where(o => o.CustomerId == customerId);
            return await Task.FromResult(customerOrders);
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            await Task.Run(() =>
            {
                order.OrderId = orders.Max(o => o.OrderId) + 1;
                orders.Add(order);
            });
            return await Task.FromResult(order.OrderId);
        }
        public async Task<int> UpdateOrderAsync(Order order, int orderId)
        {
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.TotalAmount = order.TotalAmount;
            }
            orders.Add(existingOrder!);
            return await Task.FromResult(orderId);
        }
    }
}
