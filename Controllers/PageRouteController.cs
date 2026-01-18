using Microsoft.AspNetCore.Mvc;
using SAMLitigation.Models.DynamicMenu;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class PageRouteController : Controller
    {
        private readonly DynamicMenuService dynamicMenuService;

        public PageRouteController(DynamicMenuService dynamicMenuService)
        {
            this.dynamicMenuService = dynamicMenuService;
        }

        public IActionResult Index(decimal ControllerMethodSl)
        {
            try 
            {

                WebControllerMethodDetails wbcm = dynamicMenuService.GetWebControllerMethodDetails(ControllerMethodSl);


                return RedirectToAction(wbcm.ControllerMethodName,wbcm.ControllerName);
            }
            catch 
            {
                throw;
            }
            
        }
    }
}
