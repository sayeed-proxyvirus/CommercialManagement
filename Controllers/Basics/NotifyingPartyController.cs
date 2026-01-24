using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommercialManagement.Controllers.Basics
{
    public class NotifyingPartyController : Controller
    {
        private readonly ILogger<NotifyingPartyController> _logger;
        private readonly NotifyingPartyService _notifyingPartyService;
        public NotifyingPartyController(ILogger<NotifyingPartyController> logger, NotifyingPartyService notifyingPartyService)
        {
            _logger = logger;
            _notifyingPartyService = notifyingPartyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<NotifyingParty> ListNotifyingParty = _notifyingPartyService.GetNotifyingParty();
                return View(ListNotifyingParty);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load NotifyingParty. Please try again.";
                return View(new List<NotifyingParty>());
            }
        }

        //Modal for add
        [HttpGet]
        public IActionResult GetNotifyingPartyForm()
        {
            try
            {
                //empty modal
                var model = new NotifyingParty();
                return PartialView("_NotifyingPartyForm", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add NotifyingParty form");
                return BadRequest("Failed to load form");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNotifyingParty(NotifyingParty formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var NotifyingParty = new NotifyingParty
                {
                    PartyName = formData.PartyName?.Trim(),
                    PartyCode = formData.PartyCode?.Trim(),
                    CreatedAt = DateTime.Now,
                };
                bool success = _notifyingPartyService.AddNotifyingParty(NotifyingParty);
                if (success)
                {
                    _logger.LogInformation("NotifyingParty added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"NotifyingParty '{formData.PartyName}' has been added successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("NotifyingParty data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add NotifyingParty. Please try again."
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
        public IActionResult GetNotifyingPartyForEdit(int id)
        {
            try
            {
                var NotifyingParty = _notifyingPartyService.GetbyId(id);
                if (NotifyingParty == null)
                {
                    _logger.LogWarning("NotifyingParty not found with ID: {Id}", id);
                    return NotFound("NotifyingParty not found");
                }
                return PartialView("_NotifyingPartyForm", NotifyingParty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading NotifyingParty for edit with ID: {Id}", id);
                return BadRequest("Failed to load NotifyingParty data");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNotifyingParty(NotifyingParty formData)
        {
            try
            {
                _logger.LogInformation($"Updating NotifyingParty: {formData.PartyName}");
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingParty = _notifyingPartyService.GetbyId(formData.PartyID);
                if (existingParty == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Party not found."
                    });
                }
                existingParty.PartyName = formData.PartyName?.Trim();
                existingParty.PartyCode = formData.PartyCode?.Trim();
                bool success = _notifyingPartyService.UpdateNotifyingParty(existingParty);
                if (success)
                {
                    _logger.LogInformation("Party updated Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Party '{formData.PartyName}' has been updated successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Party data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Party. Please try again."
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
