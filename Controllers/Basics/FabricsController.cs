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
                ViewBag.ListFabrics = ListFabrics.Select(x => new SelectListItem
                {
                    Value = x.ItemID.ToString(),
                    Text = x.ItemName
                }).ToList() ?? new List<SelectListItem>();
                return View();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        [HttpPost]
        public IActionResult AddFabrics(Fabrics formData) 
        {
            try
            {
                var Fabrics = new Fabrics
                {
                    ItemName = formData.ItemName,
                    ItemCode = formData.ItemCode
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
        [HttpPost]
        public IActionResult UpdateFabrics(Fabrics formData) 
        {
            try
            {
                _logger.LogInformation($"Updating Fabrics: {formData.ItemName}");
                var existingFab = _fabricsService.GetbyId(formData.ItemID);
                if (existingFab == null) 
                {
                    return Json(new
                    {
                        success = false,
                        message = "Fabrics not found."
                    });
                }
                existingFab.ItemName = formData.ItemName;
                existingFab.ItemCode = formData.ItemCode;
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
