using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Threading.Tasks;

namespace Resturant.WebUI.Components
{
    public class FeatureViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await  client.GetAsync("https://localhost:44332/api/Feature");
            var json=await responsemessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDtos>>(json);
            return View(values);
        }
    }
}
