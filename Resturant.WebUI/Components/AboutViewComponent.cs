using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components
{
    public class AboutViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
