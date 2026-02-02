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
                ViewBag.ListExportLC = _dropDownService.GetExportMainLCs();
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
                List<ExportInvoiceViewModel> exportInvoice = _exportInvoiceService.GetExportInvoices(ExpInv);
                ViewBag.ListExportLC = _dropDownService.GetExportMainLCs();
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
                if (string.IsNullOrEmpty(LCName))
                {
                    _logger.LogWarning("LCName is null or empty");
                    return PartialView("_GetInvContactInfo", new List<ExportLCViewModel>());
                }
                List<ExportLCViewModel> contacts = _exportLCItemsService.GetExportLCItems(LCName);
                return PartialView("_GetInvContactInfo", contacts ?? new List<ExportLCViewModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading contact info for LCName: {LCName}", LCName);
                return PartialView("_GetInvContactInfo", new List<ExportLCViewModel>());
            }
        }

        [HttpGet]
        public IActionResult GetContactInfo(string ExpInv)
        {
            try
            {
                var invoices = _exportInvoiceService.GetExportInvoices(ExpInv);
                var invoice = invoices?.FirstOrDefault();

                if (invoice == null || string.IsNullOrEmpty(invoice.ExpLCNo))
                {
                    return PartialView("_GetInvContactInfo", new List<ExportLCViewModel>());
                }

                // Get contacts from LC
                List<ExportLCViewModel> contacts = _exportLCItemsService.GetExportLCItems(invoice.ExpLCNo);
                return PartialView("_GetInvContactInfo", contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading contact info for ExpInv: {ExpInv}", ExpInv);
                return PartialView("_GetInvContactInfo", new List<ExportLCViewModel>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddInvoice(ExportInvoices formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var invoice = new ExportInvoices
                {
                    ExpInv = formData.ExpInv?.Trim(),
                    ExportDate = formData.ExportDate,
                    FDBPNo = formData.FDBPNo?.Trim(),
                    FDBPDate = formData.FDBPDate,
                    ShipBill = formData.ShipBill?.Trim(),
                    ShipBillDate = formData.ShipBillDate,
                    BillNo = formData.BillNo?.Trim(),
                    BillDate = formData.BillDate,
                    ExpNo = formData.ExpNo?.Trim(),
                    ExpDate = formData.ExpDate,
                    ExpLCNo = formData.ExpLCNo?.Trim()
                };

                bool success = _exportInvoiceService.AddExportInvoices(invoice);
                if (success)
                {
                    _logger.LogInformation("Export Invoice added successfully: {ExpInv}", formData.ExpInv);
                    return Json(new
                    {
                        success = true,
                        message = $"Invoice '{formData.ExpInv}' has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Export Invoice addition failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add invoice. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding export invoice");
                return Json(new
                {
                    success = false,
                    message = "An error occurred. Please try again."
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateInvoice(ExportInvoices formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingInvoice = _exportInvoiceService.GetbyId(formData.ExpInvID);

                if (existingInvoice == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invoice not found."
                    });
                }

                existingInvoice.ExpInv = formData.ExpInv?.Trim();
                existingInvoice.ExportDate = formData.ExportDate;
                existingInvoice.FDBPNo = formData.FDBPNo?.Trim();
                existingInvoice.FDBPDate = formData.FDBPDate;
                existingInvoice.ShipBill = formData.ShipBill?.Trim();
                existingInvoice.ShipBillDate = formData.ShipBillDate;
                existingInvoice.BillNo = formData.BillNo?.Trim();
                existingInvoice.BillDate = formData.BillDate;
                existingInvoice.ExpNo = formData.ExpNo?.Trim();
                existingInvoice.ExpDate = formData.ExpDate;
                existingInvoice.ExpLCNo = formData.ExpLCNo?.Trim();

                bool success = _exportInvoiceService.UpdateExportInvoices(existingInvoice);
                if (success)
                {
                    _logger.LogInformation("Export Invoice updated successfully: {ExpInv}", formData.ExpInv);
                    return Json(new
                    {
                        success = true,
                        message = $"Invoice '{formData.ExpInv}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Export Invoice update failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update invoice. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating export invoice");
                return Json(new
                {
                    success = false,
                    message = "An error occurred. Please try again."
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddExportData(ExportData formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var exportData = new ExportData
                {
                    ExpInv = formData.ExpInv?.Trim(),
                    ContactNo = formData.ContactNo?.Trim(),
                    ExpQuantity = formData.ExpQuantity,
                    UnitPrice = formData.UnitPrice
                };

                bool success = _exportDataService.AddExportData(exportData);
                if (success)
                {
                    _logger.LogInformation("Export Data added successfully for invoice: {ExpInv}", formData.ExpInv);
                    return Json(new
                    {
                        success = true,
                        message = $"Export data for contact '{formData.ContactNo}' has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Export Data addition failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add export data. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding export data");
                return Json(new
                {
                    success = false,
                    message = "An error occurred. Please try again."
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExportData(ExportData formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var allData = _exportDataService.GetExportData(formData.ExpInv);
                var existingData = allData?.FirstOrDefault(d => d.ContactNo == formData.ContactNo);

                if (existingData == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Export data not found."
                    });
                }

                existingData.ExpQuantity = formData.ExpQuantity;
                existingData.UnitPrice = formData.UnitPrice;

                bool success = _exportDataService.UpdateExportData(existingData);
                if (success)
                {
                    _logger.LogInformation("Export Data updated successfully: {ContactNo}", formData.ContactNo);
                    return Json(new
                    {
                        success = true,
                        message = $"Export data for contact '{formData.ContactNo}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Export Data update failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update export data. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating export data");
                return Json(new
                {
                    success = false,
                    message = "An error occurred. Please try again."
                });
            }
        }
    }
}