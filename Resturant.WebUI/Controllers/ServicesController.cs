using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;

namespace Resturant.WebUI.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServicesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetServices()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44332/api/Service");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDtos>>(json);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateServiceForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDtos createServiceDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createServiceDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage = await client.PostAsync("https://localhost:44332/api/Service", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetServices");
            }
            return RedirectToAction("CreateServiceForm");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/Service?id=" + id);
            return RedirectToAction("GetServices");
        }

        public async Task<IActionResult> UpdateServiceForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Service/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateServiceDtos>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDtos updateServiceDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateServiceDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Service?id=" + updateServiceDtos.ServiceId, content);
            return RedirectToAction("GetServices");
            
        }
    }
}
