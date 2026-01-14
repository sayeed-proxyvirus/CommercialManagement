using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services
{
    public interface CustomerService
    {
        List<Customer> GetCustomer();
        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        Customer GetbyId(int Id);
    }
}
