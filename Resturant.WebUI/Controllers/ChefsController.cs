using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;

namespace Resturant.WebUI.Controllers
{
    public class ChefsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetChefs()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44332/api/Chef");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(json);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateChefForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDtos createChefDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createChefDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage = await client.PostAsync("https://localhost:44332/api/Chef/", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetChefs");
            }
            return RedirectToAction("CreateChefForm");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/Chef?id=" + id);
            return RedirectToAction("GetChefs");
        }

        public async Task<IActionResult> UpdateChefForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Chef/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateChefDtos>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChef(UpdateChefDtos updateChefDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateChefDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Chef?id=" + updateChefDtos.ChefId, content);
            return RedirectToAction("GetChefs");
            return View();
        }
    }
}
