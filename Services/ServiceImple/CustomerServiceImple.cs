using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class CustomerServiceImple : CustomerService
    {
        private readonly CommercialDBContext _context;
        public CustomerServiceImple(CommercialDBContext context)
        {
            _context = context;
        }
        public Customer GetbyId(int Id)
        {
            try
            {
                var customer = _context.Customer
                    .FirstOrDefault(c => c.CustID == Id);
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Customer> GetCustomer()
        {
            try
            {
                var customers = _context.Customer
                    .FromSqlRaw("EXEC usp_ViewCust")
                    .ToList();
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddCustomer(Customer customer)
        {
            try
            {
                _context.Customer.Add(customer);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                _context.Customer.Update(customer);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
