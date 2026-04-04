using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.WebUI.Controllers
{
    public class AboutsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetAbouts()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage=await client.GetAsync("https://localhost:44332/api/About");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json=await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDtos>>(json);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateAboutForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDtos createAboutDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createAboutDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage= await client.PostAsync("https://localhost:44332/api/About/" , content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAbouts");
            }
            return RedirectToAction("CreateAboutForm");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/About?id=" + id);
            return RedirectToAction("GetAbouts");
        }

        public async Task<IActionResult> UpdateAboutForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage= await client.GetAsync("https://localhost:44332/api/About/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json=await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAboutDtos>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDtos updateAboutDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateAboutDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/About?id=" + updateAboutDtos.AboutId,content);
            return RedirectToAction("GetAbouts");
            return View();
        }
    }
}
