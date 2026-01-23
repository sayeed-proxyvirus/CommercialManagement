using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace CommercialManagement.Controllers.Basics
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, CustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Customer> ListCustomer = _customerService.GetCustomer();
                return View(ListCustomer);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load Customer. Please try again.";
                throw;
            }
        }


        //Modal Form
        [HttpGet]
        public IActionResult GetCustomerForm()
        {
            try
            {
                //empty modal
                var model = new Customer();
                return PartialView("_CustomerForm", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add Customer form");
                return BadRequest("Failed to load form");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(Customer formData) 
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var Customer = new Customer
                {
                    CustName = formData.CustName?.Trim(),
                    CustCode = formData.CustCode?.Trim(),
                    CreatedAt = DateTime.Now,
                };
                bool success = _customerService.AddCustomer(Customer);
                if (success)
                {
                    _logger.LogInformation("Customer added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Customer '{formData.CustName}' has been added successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Customer data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add Customer. Please try again."
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Edit Modal Form
        [HttpGet]
        public IActionResult GetCustomerForEdit(int id)
        {
            try
            {
                var Customer = _customerService.GetbyId(id);
                if (Customer == null)
                {
                    _logger.LogWarning("Customer not found with ID: {Id}", id);
                    return NotFound("Customer not found");
                }
                return PartialView("_CustomerForm", Customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Customer for edit with ID: {Id}", id);
                return BadRequest("Failed to load Customer data");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCustomer(Customer formData)
        {
            try
            {
                _logger.LogInformation($"Updating Customer: {formData.CustName}");
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingCust = _customerService.GetbyId(formData.CustID);
                if (existingCust == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Customer not found."
                    });
                }
                existingCust.CustName = formData.CustName?.Trim();
                existingCust.CustCode = formData.CustCode?.Trim();
                bool success = _customerService.UpdateCustomer(existingCust);
                if (success)
                {
                    _logger.LogInformation("Customer updated Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Customer '{formData.CustName}' has been updated successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Customer data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Customer. Please try again."
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
