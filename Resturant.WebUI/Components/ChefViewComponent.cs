using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;

namespace Resturant.WebUI.Components
{
    public class ChefViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var respnseMessage = await client.GetAsync("https://localhost:44332/api/Chef");
            if (respnseMessage.IsSuccessStatusCode)
            {
                var jsondata = await respnseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsondata);
                return View(values);
            }
            return View();
        }
    }
}
