using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
