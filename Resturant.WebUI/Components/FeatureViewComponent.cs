using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Components
{
    public class FeatureViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
