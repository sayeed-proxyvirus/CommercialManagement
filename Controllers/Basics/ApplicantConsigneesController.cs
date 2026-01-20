using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommercialManagement.Controllers.Basics
{
    public class ApplicantConsigneesController : Controller
    {
        private readonly ILogger<ApplicantConsigneesController> _logger;
        private readonly ApplicantConsigneesService _applicantConsigneesService;
        public ApplicantConsigneesController(ILogger<ApplicantConsigneesController> logger, ApplicantConsigneesService applicantConsigneesService)
        {
            _logger = logger;
            _applicantConsigneesService = applicantConsigneesService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ApplicantConsignees> ListApplicantConsignees = _applicantConsigneesService.GetApplicantConsignees();
                return View(ListApplicantConsignees);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load Applicant Consignees. Please try again.";
                return View(new List<ApplicantConsignees>());
            }

        }
        //Modal Form
        [HttpGet]
        public IActionResult GetApplicantForm()
        {
            try
            {
                //empty modal
                var model = new ApplicantConsignees();
                return PartialView("_ApplicantForm", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add ApplicantConsignees form");
                return BadRequest("Failed to load form");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddApplicantConsignees(ApplicantConsignees formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var ApplicantConsignees = new ApplicantConsignees
                {
                    ApplicantName = formData.ApplicantName?.Trim(),
                    ApplicantCode = formData.ApplicantCode?.Trim(),
                    CreatedAt = DateTime.Now,
                };
                bool success = _applicantConsigneesService.AddApplicantConsignees(ApplicantConsignees);
                if (success)
                {
                    _logger.LogInformation("ApplicantConsignees added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Applicant '{formData.ApplicantName}' has been added successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("ApplicantConsignee data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add ApplicantConsignee. Please try again."
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
        public IActionResult GetApplicantForEdit(int id)
        {
            try
            {
                var ApplicantConsignees = _applicantConsigneesService.GetbyId(id);
                if (ApplicantConsignees == null)
                {
                    _logger.LogWarning("ApplicantConsignees not found with ID: {Id}", id);
                    return NotFound("ApplicantConsignees not found");
                }
                return PartialView("_ApplicantForm", ApplicantConsignees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ApplicantConsignees for edit with ID: {Id}", id);
                return BadRequest("Failed to load ApplicantConsignees data");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateApplicantConsignees(ApplicantConsignees formData)
        {
            try
            {
                _logger.LogInformation($"Updating ApplicantConsignees: {formData.ApplicantName}");
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingApplicant = _applicantConsigneesService.GetbyId(formData.ApplicantID);
                if (existingApplicant == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "ApplicantConsignees not found."
                    });
                }
                existingApplicant.ApplicantName = formData.ApplicantName?.Trim();
                existingApplicant.ApplicantCode = formData.ApplicantCode?.Trim();
                bool success = _applicantConsigneesService.UpdateApplicantConsignees(existingApplicant);
                if (success)
                {
                    _logger.LogInformation("ApplicantConsignees updated Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Applicant '{formData.ApplicantName}' has been updated successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Applicant data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Applicant. Please try again."
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
