using CustomerOrder.Application.DTOs.Customer;
using CustomerOrder.Domain.Entities;

namespace CustomerOrder.Application.Interfaces.Customers
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<int> UpdateCustomerAsync(Customer customer, int customerId);
    }
}
