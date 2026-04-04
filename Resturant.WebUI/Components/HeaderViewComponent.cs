using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components
{
    public class HeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
