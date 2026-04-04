using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Threading.Tasks;

namespace Resturant.WebUI.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PhotosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetPhotos()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:44332/api/Photo");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsons=await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPhotoDtos>>(jsons);
                return View(values);
            }
            return View();
        }
    }
}
