using Microsoft.AspNetCore.Mvc;

namespace Resturant.WebUI.Controllers
{
    public class DashbordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
