using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components.AdminLayoutViewComponent
{
    public class HeadAdminViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
