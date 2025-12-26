using CustomerOrder.Application.DTOs.Customer;
using CustomerOrder.Application.DTOs.Order;
using CustomerOrder.Application.Interfaces.Customers;
using CustomerOrder.Application.Interfaces.Orders;
using CustomerOrder.Domain.Entities;

namespace CustomerOrder.Application.Services.Orders
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        public async Task<int> CreateOrderAsync(OrderDto dto)
        {
            //Orders must have a total amount greater than zero.
            if (dto.TotalAmount <= 0)
            {
                throw new Exception("Order total amount must be greater than zero.");
            }
            // Orders must not be created or updated for non-existing customers. 
            var customer = await _customerRepository.GetCustomerByIdAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new InvalidOperationException("Cannot create order for non-existing customer.");
            }
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount
            };
            return await _orderRepository.CreateOrderAsync(order);

        }
        public async Task<int> UpdateOrderAsync(OrderDto dto, int orderId)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount
            };
            return await _orderRepository.UpdateOrderAsync(order, orderId);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount
            });
        }
    }
}
