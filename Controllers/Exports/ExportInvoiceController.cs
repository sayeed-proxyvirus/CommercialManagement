using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.ViewModels;
using CommercialManagement.Services.DropDownSerivces;
using CommercialManagement.Services.Exports;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CommercialManagement.Controllers.Exports
{
    public class ExportInvoiceController : Controller
    {
        private readonly ILogger<ExportInvoiceController> _logger;
        private readonly ExportDataService _exportDataService;
        private readonly ExportInvoiceService _exportInvoiceService;
        private readonly ExportLCItemsService _exportLCItemsService;
        private readonly DropDownService _dropDownService;
        public ExportInvoiceController(ILogger<ExportInvoiceController> logger, ExportLCItemsService exportLCItemsService, ExportDataService exportDataService, ExportInvoiceService exportInvoiceService, DropDownService dropDownService)
        {
            _logger = logger;
            _exportDataService = exportDataService;
            _exportInvoiceService = exportInvoiceService;
            _exportLCItemsService = exportLCItemsService;
            _dropDownService = dropDownService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ExportInvoices> exportInvoices = _dropDownService.GetExportInvoices();
                return View(exportInvoices);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load Invoice. Please try again.";
                return View(new List<ExportInvoices>());
            }
        }
        [HttpGet]
        public IActionResult GetExpInvInfo(string ExpInv)
        {
            try
            {
                ExpInv = "M00L/INV/1427/08";
                List<ExportInvoiceViewModel> exportInvoice = _exportInvoiceService.GetExportInvoices(ExpInv);
                return PartialView("_GetInvInfo", exportInvoice);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetInvContactInfo(string LCName)
        {
            try
            {
                //LCName = "C1B36819643";
                List<ExportLCViewModel> exportMainContacts = _exportLCItemsService.GetExportLCItems(LCName);
                return PartialView("_GetInvContactInfo", exportMainContacts);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetExpInvContactInfo(string ExpInv)
        {
            try
            {
                ExpInv = "M00L/INV/1427/08";
                List<ExportData> exportInvoice = _exportDataService.GetExportData(ExpInv);
                return PartialView("_GetInvInfo", exportInvoice);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
