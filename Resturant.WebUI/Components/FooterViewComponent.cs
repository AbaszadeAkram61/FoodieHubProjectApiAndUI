using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components
{
    public class FooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
