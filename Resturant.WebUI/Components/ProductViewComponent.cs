using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;

namespace Resturant.WebUI.Components
{
    public class ProductViewComponent:ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ProductViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Product");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsondatastring =await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsondatastring);
                return View(values);
            }
            return View();
        }
    }
}
