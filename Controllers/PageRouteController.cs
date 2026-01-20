using Microsoft.AspNetCore.Mvc;
using CommercialManagement.Models.DynamicMenu;
using SAMLitigation.Services;
using CommercialManagement.Filters;

namespace SAMLitigation.Controllers
{
    [SessionAuthorization]
    public class PageRouteController : Controller
    {
        private readonly DynamicMenuService dynamicMenuService;
        private readonly ILogger<PageRouteController> _logger;

        public PageRouteController(DynamicMenuService dynamicMenuService, ILogger<PageRouteController> logger)
        {
            this.dynamicMenuService = dynamicMenuService;
            this._logger = logger;
        }

        public IActionResult Index(decimal ControllerMethodSl)
        {
            try
            {
                WebControllerMethodDetails wbcm = dynamicMenuService.GetWebControllerMethodDetails(ControllerMethodSl);

                if (wbcm == null)
                {
                    _logger.LogWarning("Controller method not found for SL: {SL}", ControllerMethodSl);
                    return NotFound();
                }

                return RedirectToAction(wbcm.ControllerMethodName, wbcm.ControllerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PageRoute for SL: {SL}", ControllerMethodSl);
                throw;
            }
        }
    }
}