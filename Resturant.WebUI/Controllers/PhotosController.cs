using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;
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


        public IActionResult CreatePhotoForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhoto(CreatePhotoDtos createPhotoDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createPhotoDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage = await client.PostAsync("https://localhost:44332/api/Photo/", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetPhotos");
            }
            return RedirectToAction("CreatePhotoForm");
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/Photo?id=" + id);
            return RedirectToAction("GetPhotos");
        }

        public async Task<IActionResult> UpdatePhotoForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Photo/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdatePhotoDtos>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(UpdatePhotoDtos updatePhotoDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updatePhotoDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Photo?id=" + updatePhotoDtos.PhotoId, content);
            return RedirectToAction("GetPhotos");
            
        }
    }
}
