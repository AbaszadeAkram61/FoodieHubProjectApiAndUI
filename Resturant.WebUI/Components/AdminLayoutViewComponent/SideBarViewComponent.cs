using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components.AdminLayoutViewComponent
{
    public class SideBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
