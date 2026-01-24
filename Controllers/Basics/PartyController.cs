using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommercialManagement.Controllers.Basics
{
    public class PartyController : Controller
    {
        private readonly ILogger<PartyController> _logger;
        private readonly PartyService _partyService;
        public PartyController(ILogger<PartyController> logger, PartyService partyService)
        {
            _logger = logger;
            _partyService = partyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Party> ListParty = _partyService.GetParty();
                return View(ListParty);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load Party. Please try again.";
                return View(new List<Party>());
            }
        }

        //Modal for add
        [HttpGet]
        public IActionResult GetPartyForm()
        {
            try
            {
                //empty modal
                var model = new Party();
                return PartialView("_PartyForm", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add Party form");
                return BadRequest("Failed to load form");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParty(Party formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var Party = new Party
                {
                    PartyName = formData.PartyName?.Trim(),
                    PartyCode = formData.PartyCode?.Trim(),
                    CreatedAt = DateTime.Now,
                };
                bool success = _partyService.AddParty(Party);
                if (success)
                {
                    _logger.LogInformation("Party added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Party '{formData.PartyName}' has been added successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Party data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add Party. Please try again."
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
        public IActionResult GetPartyForEdit(int id)
        {
            try
            {
                var Party = _partyService.GetbyId(id);
                if (Party == null)
                {
                    _logger.LogWarning("Party not found with ID: {Id}", id);
                    return NotFound("Party not found");
                }
                return PartialView("_PartyForm", Party);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Party for edit with ID: {Id}", id);
                return BadRequest("Failed to load Party data");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateParty(Party formData)
        {
            try
            {
                _logger.LogInformation($"Updating Party: {formData.PartyName}");
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingParty = _partyService.GetbyId(formData.PartyID);
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
                bool success = _partyService.UpdateParty(existingParty);
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
