using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.ViewModels;
using CommercialManagement.Services;
using CommercialManagement.Services.DropDownSerivces;
using CommercialManagement.Services.Exports;
using Microsoft.AspNetCore.Mvc;

namespace CommercialManagement.Controllers.Exports
{
    public class ExportMainController : Controller
    {
        private readonly ILogger<ExportMainController> _logger;
        private readonly ExportMainService _exportMainService;
        private readonly DropDownService _dropDownService;
        public ExportMainController(ILogger<ExportMainController> logger, ExportMainService exportMainService, NotifyingPartyService notifyingPartySerivce, ApplicantConsigneesService applicantConsigneesService, BeneficiaryService beneficiaryService, DropDownService dropDownService)
        {
            _logger = logger;
            _exportMainService = exportMainService;

            _dropDownService = dropDownService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ExportMain> exportMainLCs = _dropDownService.GetExportMainLCs();
                return View(exportMainLCs);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load MainExpName. Please try again.";
                return View(new List<ExportMain>());
            }
        }
        [HttpPost]
        public IActionResult GetLCInfo(string LCName) 
        {
            List<ExportMainViewModel> exportMainLCs = _exportMainService.GetExportMain(LCName);
            ViewBag.ListBeneficiary = _dropDownService.GetBeneficiary();
            ViewBag.ListNotify = _dropDownService.GetNotifyingParty();
            ViewBag.ListApplicant = _dropDownService.GetApplicantConsignees();
            return PartialView("_GetLCInfo", exportMainLCs);
        }
        //private LoadDropdowns
    }
}
