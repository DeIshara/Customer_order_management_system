using CustomerOrder.Application.DTOs.Customer;
using CustomerOrder.Application.Interfaces.Customers;
using CustomerOrderManagementSystem.API.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerOrderManagementSystem.API.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = new ApiResponse<IEnumerable<CustomerDto>>();
            try
            {
                response.Data = await _customerService.GetAllCustomersAsync();
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
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerDto customer)
        {
            var response = new ApiResponse<int>();
            try
            {
                var customerId = await _customerService.CreateCustomerAsync(customer);
                response.Data = customerId;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;
                response.Message = "Customer created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = $"An error occurred while creating the customer: {ex.Message}";
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int customerId, [FromBody] CustomerDto customer)
        {
            var response = new ApiResponse<int>();
            try
            {
                var updatedCustomerId = await _customerService.UpdateCustomerAsync(customer, customerId);
                response.Data = updatedCustomerId;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = "Customer updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = $"An error occurred while updating the customer: {ex.Message}";
            }
            return Ok(response);
        }
    }
}
