using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommercialManagement.Controllers.Basics
{
    public class BeneficiaryController : Controller
    {
        private readonly ILogger<BeneficiaryController> _logger;
        private readonly BeneficiaryService _beneficiaryService;
        public BeneficiaryController(ILogger<BeneficiaryController> logger, BeneficiaryService beneficiaryService)
        {
            _logger = logger;
            _beneficiaryService = beneficiaryService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Beneficiary> ListBeneficiary = _beneficiaryService.GetBeneficiary();
                return View(ListBeneficiary);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load Beneficiary. Please try again.";
                return View(new List<Beneficiary>());
            }

        }


        //Modal Form
        [HttpGet]
        public IActionResult GetBeneficiaryForm()
        {
            try
            {
                //empty modal
                var model = new Beneficiary();
                return PartialView("_BeneficiaryForm", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add Beneficiarys form");
                return BadRequest("Failed to load form");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBeneficiary(Beneficiary formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var Beneficiary = new Beneficiary
                {
                    BenName = formData.BenName?.Trim(),
                    BenCode = formData.BenCode?.Trim(),
                    Remarks = formData.Remarks?.Trim(),
                    Address = formData.Address?.Trim(),
                    CreatedAt = DateTime.Now,
                };
                bool success = _beneficiaryService.AddBeneficiary(Beneficiary);
                if (success)
                {
                    _logger.LogInformation("Beneficiary added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Beneficiary '{formData.BenName}' has been added successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Beneficiary data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add Beneficiary. Please try again."
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
        public IActionResult GetBeneficiaryForEdit(int id)
        {
            try
            {
                var Beneficiary = _beneficiaryService.GetbyId(id);
                if (Beneficiary == null)
                {
                    _logger.LogWarning("Beneficiary not found with ID: {Id}", id);
                    return NotFound("Beneficiary not found");
                }
                return PartialView("_BeneficiaryForm", Beneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Beneficiary for edit with ID: {Id}", id);
                return BadRequest("Failed to load Beneficiary data");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateBeneficiarys(Beneficiary formData)
        {
            try
            {
                _logger.LogInformation($"Updating Beneficiary: {formData.BenName}");
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingBen = _beneficiaryService.GetbyId(formData.BenID);
                if (existingBen == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Beneficiary not found."
                    });
                }
                existingBen.BenName = formData.BenName?.Trim();
                existingBen.BenCode = formData.BenCode?.Trim();
                existingBen.Address = formData.Address?.Trim();
                existingBen.Remarks = formData.Remarks?.Trim();
                bool success = _beneficiaryService.UpdateBeneficiary(existingBen);
                if (success)
                {
                    _logger.LogInformation("Beneficiary updated Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Beneficiary '{formData.BenName}' has been updated successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Beneficiary data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Beneficiary. Please try again."
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
