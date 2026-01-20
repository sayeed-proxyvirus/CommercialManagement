using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommercialManagement.Controllers.Basics
{
    public class FabricsController : Controller
    {
        private readonly ILogger<FabricsController> _logger;
        private readonly FabricsService _fabricsService;
        public FabricsController(ILogger<FabricsController> logger, FabricsService fabricsService)
        {
            _logger = logger;
            _fabricsService = fabricsService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Fabrics> ListFabrics = _fabricsService.GetFabrics();
                return View(ListFabrics);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load fabrics. Please try again.";
                return View(new List<Fabrics>());
            }
            
        }
        //Modal Form
        [HttpGet]
        public IActionResult GetFabricForm()
        {
            try
            {
                //empty modal
                var model = new Fabrics();
                return PartialView("_FabricForm", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add fabric form");
                return BadRequest("Failed to load form");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFabrics(Fabrics formData) 
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var Fabrics = new Fabrics
                {
                    ItemName = formData.ItemName?.Trim(),
                    ItemCode = formData.ItemCode?.Trim(),
                    CreatedAt = DateTime.Now,
                };
                bool success = _fabricsService.AddFabrics(Fabrics);
                if (success)
                {
                    _logger.LogInformation("Fabrics added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Fabric '{formData.ItemName}' has been added successfully!!!"
                    });
                }
                else 
                {
                    _logger.LogWarning("Fabric data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add Fabric. Please try again."
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
        public IActionResult GetFabricForEdit(int id)
        {
            try
            {
                var fabric = _fabricsService.GetbyId(id);
                if (fabric == null)
                {
                    _logger.LogWarning("Fabric not found with ID: {Id}", id);
                    return NotFound("Fabric not found");
                }
                return PartialView("_FabricForm", fabric);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading fabric for edit with ID: {Id}", id);
                return BadRequest("Failed to load fabric data");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFabrics(Fabrics formData) 
        {
            try
            {
                _logger.LogInformation($"Updating Fabrics: {formData.ItemName}");
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingFab = _fabricsService.GetbyId(formData.ItemID);
                if (existingFab == null) 
                {
                    return Json(new
                    {
                        success = false,
                        message = "Fabrics not found."
                    });
                }
                existingFab.ItemName = formData.ItemName?.Trim();
                existingFab.ItemCode = formData.ItemCode?.Trim();
                bool success = _fabricsService.UpdateFabrics(existingFab);
                if (success)
                {
                    _logger.LogInformation("Fabrics updated Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Fabric '{formData.ItemName}' has been updated successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("Fabric data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Fabric. Please try again."
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
