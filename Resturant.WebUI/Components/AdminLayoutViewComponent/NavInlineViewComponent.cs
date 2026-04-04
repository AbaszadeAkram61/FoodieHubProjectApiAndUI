using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components.AdminLayoutViewComponent
{
    public class NavInlineViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
