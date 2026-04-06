using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;

namespace Resturant.WebUI.Components
{
    public class GalereyaViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalereyaViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Photo");
            var json = await responsemessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultPhotoDtos>>(json);
            return View(values);
        }
    }
}
