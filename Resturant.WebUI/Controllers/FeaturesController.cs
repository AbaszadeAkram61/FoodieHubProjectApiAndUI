using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.WebUI.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeaturesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetFeatures()
        {
            var client = _httpClientFactory.CreateClient();
            var responsmessage=await client.GetAsync("https://localhost:44332/api/Feature");
            if (responsmessage.IsSuccessStatusCode)
            {
                var json=await responsmessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDtos>>(json);
                return View(values);
            }


            return View();
        }

        public IActionResult CreateFeatureForm()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await  client.DeleteAsync("https://localhost:44332/api/Feature?id=" + id);
            return RedirectToAction("GetFeatures");
        }

        [HttpGet]
        public  async Task<IActionResult> UpdateFeatureForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:44332/api/Feature/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json= await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateFeatureDtos>(json);
                return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDtos updateFeatureDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata= JsonConvert.SerializeObject(updateFeatureDtos);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Feature?id=" + updateFeatureDtos.FeatureId, content);
            return RedirectToAction("GetFeatures");
        }

    }
}
