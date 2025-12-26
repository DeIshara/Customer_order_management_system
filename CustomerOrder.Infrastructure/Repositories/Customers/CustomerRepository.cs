using CustomerOrder.Application.Interfaces.Customers;
using CustomerOrder.Domain.Entities;

namespace CustomerOrder.Infrastructure.Repositories.Customers
{
    public class CustomerRepository: ICustomerRepository
    {
        public CustomerRepository() { }

        List<Customer> customers = new List<Customer>
        {
            new Customer { CustomerId = 1, Name = "Alice Johnson", Email = "id1@gmail.com" },
            new Customer { CustomerId = 2, Name = "Bob Smith", Email = "id2@gmail.com" },
            new Customer { CustomerId = 3, Name = "Charlie Brown", Email = "id3@gmail.com" }
        };
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await Task.FromResult(customers);
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            await Task.Run(() =>
            {
                customer.CustomerId = customers.Max(c => c.CustomerId) + 1;
                customers.Add(customer);
            });
            return await Task.FromResult(customer.CustomerId);
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
           var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
           return await Task.FromResult(customer);
        }

        public async Task<int> UpdateCustomerAsync(Customer customer, int customerId)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
            }
            customers.Add(existingCustomer!);
            return await Task.FromResult(customerId);
        }
    }
}
