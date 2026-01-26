using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.ViewModels;
using CommercialManagement.Services;
using CommercialManagement.Services.DropDownSerivces;
using CommercialManagement.Services.Exports;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CommercialManagement.Controllers.Exports
{
    public class ExportMainController : Controller
    {
        private readonly ILogger<ExportMainController> _logger;
        private readonly ExportMainService _exportMainService;
        private readonly ExportLCItemsService _exportLCItemsService;
        private readonly DropDownService _dropDownService;
        public ExportMainController(ILogger<ExportMainController> logger, ExportLCItemsService exportLCItemsService, ExportMainService exportMainService, NotifyingPartyService notifyingPartySerivce, ApplicantConsigneesService applicantConsigneesService, BeneficiaryService beneficiaryService, DropDownService dropDownService)
        {
            _logger = logger;
            _exportMainService = exportMainService;
            _exportLCItemsService = exportLCItemsService;
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
        [HttpGet]
        public IActionResult GetLCInfo(string LCName) 
        {
            List<ExportMainViewModel> exportMainLCs = _exportMainService.GetExportMain(LCName);
            ViewBag.ListBeneficiary = _dropDownService.GetBeneficiary();
            ViewBag.ListNotify = _dropDownService.GetNotifyingParty();
            ViewBag.ListApplicant = _dropDownService.GetApplicantConsignees();
            return PartialView("_GetLCInfo", exportMainLCs);
        }
        [HttpGet]
        public IActionResult GetContactInfo(string LCName)
        {
            List<ExportLCViewModel> exportMainContacts = _exportLCItemsService.GetExportLCItems(LCName);
            return PartialView("_GetContactInfo", exportMainContacts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLCs(ExportMain formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var ExportLCs = new ExportMain 
                {
                    MainExpName = formData.MainExpName?.Trim(),
                    ReceiveDate = formData.ReceiveDate,
                    ExpDate = formData.ExpDate,
                    Dollars = formData.Dollars,
                    MainExpTaka = formData.MainExpTaka,
                    ExpQuanity = formData.ExpQuanity,
                    ApplicantID = formData.ApplicantID,
                    BenID = formData.BenID,
                    PartyID = formData.PartyID,
                    Destination = formData.Destination?.Trim()
                };
                bool success = _exportMainService.AddExportMain(ExportLCs);
                if (success) 
                {
                    _logger.LogInformation("LC added Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"LC '{formData.MainExpName}' has been added successfully!!!"
                    });
                } 
                else 
                {
                    _logger.LogWarning("LC data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add LC. Please try again."
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateLCs(ExportMain formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingLC = _exportMainService.GetbyId(formData.ExpID);
                if (existingLC == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "LC not found."
                    });
                }

                existingLC.MainExpName = formData.MainExpName?.Trim();
                existingLC.ReceiveDate = formData.ReceiveDate;
                existingLC.ExpDate = formData.ExpDate;
                existingLC.Dollars = formData.Dollars;
                existingLC.MainExpTaka = formData.MainExpTaka;
                existingLC.ExpQuanity = formData.ExpQuanity;
                existingLC.ApplicantID = formData.ApplicantID;
                existingLC.BenID = formData.BenID;
                existingLC.PartyID = formData.PartyID;
                existingLC.Destination = formData.Destination?.Trim();
                bool success = _exportMainService.UpdateExportMain(existingLC);
                if (success)
                {
                    _logger.LogInformation("LC updated Successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"LC '{formData.MainExpName}' has been updated successfully!!!"
                    });
                }
                else
                {
                    _logger.LogWarning("LC data Update has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update LC. Please try again."
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
