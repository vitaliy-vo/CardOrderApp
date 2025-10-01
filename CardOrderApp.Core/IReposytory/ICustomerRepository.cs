using CardOrderApp.Core.Dtos.Customer;

namespace CardOrderApp.Core.Interfaces
{
    public interface ICustomerRepository
    {
        List<CustomerDto> GetAllCustomers();
        int CreateCustomer(CustomerDto customer);
        CustomerDto GetCustomerById(int id);
        CustomerDto GetCustomerWithAllDomementsById(int id);
        void UnAnctiveCustomer(CustomerDto customer);
        void UpdateCustomer(CustomerDto customer);
    }
}