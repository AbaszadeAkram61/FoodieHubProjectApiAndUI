using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components.AdminLayoutViewComponent
{
    public class NavViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
