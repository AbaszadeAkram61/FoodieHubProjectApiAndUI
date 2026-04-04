using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components
{
    public class MenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
