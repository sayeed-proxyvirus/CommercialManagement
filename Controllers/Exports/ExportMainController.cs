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
        private readonly BeneficiaryService _beneficiaryService;
        private readonly NotifyingPartyService _notifyingPartySerivce;
        private readonly ApplicantConsigneesService _applicantConsigneesService;
        private readonly DropDownService _dropDownService;
        public ExportMainController(ILogger<ExportMainController> logger, ExportMainService exportMainService, NotifyingPartyService notifyingPartySerivce, ApplicantConsigneesService applicantConsigneesService, BeneficiaryService beneficiaryService, DropDownService dropDownService)
        {
            _logger = logger;
            _exportMainService = exportMainService;
            _notifyingPartySerivce = notifyingPartySerivce;
            _applicantConsigneesService = applicantConsigneesService;
            _beneficiaryService = beneficiaryService;
            _dropDownService = dropDownService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ExportMain> exportMainLCs = _dropDownService.GetExportMainLCs();
                List<Beneficiary> ListBeneficiary = _dropDownService.GetBeneficiary();
                List<NotifyingParty> ListNotify = _dropDownService.GetNotifyingParty();
                List<ApplicantConsignees> ListApplicant = _dropDownService.GetApplicantConsignees();
                return View(exportMainLCs);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load MainExpName. Please try again.";
                return View(new List<ExportMain>());
            }
        }
        //private LoadDropdowns
    }
}
