using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;

namespace Resturant.WebUI.Components
{
    public class EventViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var respnseMessage = await client.GetAsync("https://localhost:44332/api/Event");
            if (respnseMessage.IsSuccessStatusCode)
            {
                var jsondata = await respnseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEventsDto>>(jsondata);
                return View(values);
            }
            return View();
        }
    }
}
