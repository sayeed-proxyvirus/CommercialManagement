using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.ViewModels;
using CommercialManagement.Services.Exports;
using Microsoft.AspNetCore.Mvc;

namespace CommercialManagement.Controllers.Exports
{
    public class ExportMainController : Controller
    {
        private readonly ILogger<ExportMainController> _logger;
        private readonly ExportMainService _exportMainService;
        public ExportMainController(ILogger<ExportMainController> logger, ExportMainService exportMainService)
        {
            _logger = logger;
            _exportMainService = exportMainService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ExportMain> exportMainLCs = _exportMainService.GetExportMainLCs();
                return View(exportMainLCs);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load MainExpName. Please try again.";
                return View(new List<ExportMain>());
            }
        }
    }
}
