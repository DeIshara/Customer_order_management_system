using CustomerOrder.Application.DTOs.Customer;

namespace CustomerOrder.Application.Interfaces.Customers
{
    public interface ICustomerService
    {
        Task<int> CreateCustomerAsync(CustomerDto customer);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int customerId);
        Task<int> UpdateCustomerAsync(CustomerDto customer, int customerId);
    }
}
//The system must not allow duplicate customer emails. 