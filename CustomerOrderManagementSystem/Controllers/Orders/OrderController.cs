using CustomerOrder.Application.DTOs.Order;
using CustomerOrder.Application.Interfaces.Orders;
using CustomerOrderManagementSystem.API.Shared;
using Microsoft.AspNetCore.Mvc;


namespace CustomerOrderManagementSystem.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersByCustomerIdAsync(int customerId)
        {
            var response = new ApiResponse<IEnumerable<OrderDto>>();
            try
            {
                response.Data = await _orderService.GetOrdersByCustomerIdAsync(customerId);
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = "Customers retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = $"An error occurred while retrieving customers: {ex.Message}";
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto order)
        {
            var response = new ApiResponse<int>();
            try
            {
                var customerId = await _orderService.CreateOrderAsync(order);
                response.Data = customerId;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;
                response.Message = "Order created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = $"An error occurred while creating the order: {ex.Message}";
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int orderId, [FromBody] OrderDto order)
        {
            var response = new ApiResponse<int>();
            try
            {
                var updateOrderId = await _orderService.UpdateOrderAsync(order, orderId);
                response.Data = updateOrderId;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = "Order updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = $"An error occurred while updating the order: {ex.Message}";
            }
            return Ok(response);
        }
    }
}
