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
        private readonly DropDownService _dropDownService;
        public StyleInfoController(ILogger<StyleInfoController> logger, StyleInfoService styleInfoService, DropDownService dropDownService)
        {
            _logger = logger;
            _styleInfoService = styleInfoService;
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

                contno = "05/057J";
                List<StyleInfoViewModel> styleInfos = _styleInfoService.GetStyle(contno);
                return PartialView("_GetStyleInfo", styleInfos);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
