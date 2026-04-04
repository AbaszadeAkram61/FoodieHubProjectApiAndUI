using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components
{
    public class HeadViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
