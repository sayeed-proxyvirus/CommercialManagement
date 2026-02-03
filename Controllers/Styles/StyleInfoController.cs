using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Styles;
using CommercialManagement.Models.ViewModels;
using CommercialManagement.Services.DropDownSerivces;
using CommercialManagement.Services.Exports;
using CommercialManagement.Services.Style;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CommercialManagement.Controllers.Styles
{
    public class StyleInfoController : Controller
    {
        private readonly ILogger<StyleInfoController> _logger;
        private readonly StyleInfoService _styleInfoService;
        private readonly StyleTransService _styletransService;
        private readonly DropDownService _dropDownService;
        public StyleInfoController(ILogger<StyleInfoController> logger,StyleTransService styleTransService, StyleInfoService styleInfoService, DropDownService dropDownService)
        {
            _logger = logger;
            _styleInfoService = styleInfoService;
            _styletransService = styleTransService;
            _dropDownService = dropDownService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<StyleInfo> contacts = _dropDownService.GetStyleInfos();
                ViewBag.ListCustomer = _dropDownService.GetCustomer();
                ViewBag.ListBeneficiary = _dropDownService.GetBeneficiary();
                //ViewBag.ListNotify = _dropDownService.GetNotifyingParty();
                return View(contacts);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Failed to load ContactNo. Please try again.";
                return View(new List<StyleInfo>());
            }
        }
        [HttpGet]
        public IActionResult GetContactInfo(string contno) 
        {
            try
            {

                //contno = "05/057J";
                List<StyleInfoViewModel> styleInfos = _styleInfoService.GetStyle(contno);
                ViewBag.ListCustomer = _dropDownService.GetCustomer();
                ViewBag.ListBeneficiary = _dropDownService.GetBeneficiary();
                return PartialView("_GetStyleInfo", styleInfos);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error loading contact info for: {ContactNo}", contno);
                return PartialView("_GetStyleInfo", new List<StyleInfoViewModel>());
            }
        }
        [HttpGet]
        public IActionResult GetTransInfo(string contno)
        {
            try
            {

                List<StyleTransViewModel> styleTrans = _styletransService.GetStyleTrans(contno);
                ViewBag.ListItem = _dropDownService.GetFabrics();
                return PartialView("_GetStyleTrans", styleTrans);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error loading contact info for: {ContactNo}", contno);
                return PartialView("_GetStyleInfo", new List<StyleInfoViewModel>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStyle(StyleInfo formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var styleInfo = new StyleInfo
                {
                    ContactID = formData.ContactID,
                    ContactNo = formData.ContactNo?.Trim(),
                    StyleDesc = formData.StyleDesc?.Trim(),
                    StyleName = formData.StyleName?.Trim(),
                    CustID = formData.CustID,
                    BenID = formData.BenID,
                    Wash = formData.Wash?.Trim(),
                    OrderNo = formData.OrderNo?.Trim(),
                    TotalQuantity = formData.TotalQuantity,
                    TotalFabricQuantity = formData.TotalFabricQuantity,
                    ShipmentDate = formData.ShipmentDate,
                    StyleCM = formData.StyleCM,
                    Lbi = formData.Lbi,
                    WashingCharge = formData.WashingCharge,
                    FabricDozens = formData.FabricDozens,
                    FabricsPerPcs = formData.FabricsPerPcs,
                    LACPerPcs = formData.LACPerPcs
                };

                bool success = _styleInfoService.AddStyles(styleInfo);
                if (success)
                {
                    _logger.LogInformation("Style Info added successfully: {ContactNo}", formData.ContactNo);
                    return Json(new
                    {
                        success = true,
                        message = $"Style Info for contract '{formData.ContactNo}' has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Style Info addition failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add style info. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding style info");
                return Json(new
                {
                    success = false,
                    message = "An error occurred. Please try again."
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStyle(StyleInfo formData)
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName") ?? "System";
                var existingStyle = _styleInfoService.GetbyId(formData.ContactID ?? 0);

                if (existingStyle == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Style Info not found."
                    });
                }
                existingStyle.ContactID = formData.ContactID;
                existingStyle.ContactNo = formData.ContactNo?.Trim();
                existingStyle.StyleDesc = formData.StyleDesc?.Trim();
                existingStyle.StyleName = formData.StyleName?.Trim();
                existingStyle.CustID = formData.CustID;
                existingStyle.BenID = formData.BenID;
                existingStyle.Wash = formData.Wash?.Trim();
                existingStyle.OrderNo = formData.OrderNo?.Trim();
                existingStyle.TotalQuantity = formData.TotalQuantity;
                existingStyle.TotalFabricQuantity = formData.TotalFabricQuantity;
                existingStyle.ShipmentDate = formData.ShipmentDate;
                existingStyle.StyleCM = formData.StyleCM;
                existingStyle.Lbi = formData.Lbi;
                existingStyle.WashingCharge = formData.WashingCharge;
                existingStyle.FabricDozens = formData.FabricDozens;
                existingStyle.FabricsPerPcs = formData.FabricsPerPcs;
                existingStyle.LACPerPcs = formData.LACPerPcs;

                bool success = _styleInfoService.UpdateStyle(existingStyle);
                if (success)
                {
                    _logger.LogInformation("Style Info updated successfully: {ContactNo}", formData.ContactNo);
                    return Json(new
                    {
                        success = true,
                        message = $"Style Info for contract '{formData.ContactNo}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Style Info update failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update style info. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating style info");
                return Json(new
                {
                    success = false,
                    message = "An error occurred. Please try again."
                });
            }
        }
    }
}
