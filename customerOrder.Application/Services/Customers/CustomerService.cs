using CustomerOrder.Application.DTOs.Customer;
using CustomerOrder.Application.Interfaces.Customers;
using CustomerOrder.Domain.Entities;

namespace CustomerOrder.Application.Services.Customers
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Email = c.Email
            });
        }
        public async Task<int> CreateCustomerAsync(CustomerDto dto)
        {
            //The system must not allow duplicate customer emails. 
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(dto.CustomerId);
            if (existingCustomer?.Email == dto.Email)
            {
                throw new Exception("A customer with the same email already exists.");
            }
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email
            };
            return await _customerRepository.CreateCustomerAsync(customer);
        }
        public async Task<int> UpdateCustomerAsync(CustomerDto dto, int customerId)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email
            };
            return await _customerRepository.UpdateCustomerAsync(customer, customerId);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (existingCustomer == null)
                return null;

            return new CustomerDto
            {
                CustomerId = existingCustomer.CustomerId,
                Name = existingCustomer.Name,
                Email = existingCustomer.Email
            };
        }
    }
}
