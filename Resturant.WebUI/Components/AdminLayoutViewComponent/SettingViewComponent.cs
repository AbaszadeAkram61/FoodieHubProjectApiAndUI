using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components.AdminLayoutViewComponent
{
    public class SettingViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
