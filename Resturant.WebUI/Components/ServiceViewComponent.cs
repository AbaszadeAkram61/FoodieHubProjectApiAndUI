using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.WebApi.Dtos.Service;
using Resturant.WebUI.Models.Dto;
using System.Threading.Tasks;

namespace Resturant.WebUI.Components
{
    public class ServiceViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var respnseMessage = await client.GetAsync("https://localhost:44332/api/Service");
            if (respnseMessage.IsSuccessStatusCode)
            {
                var jsondata = await respnseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDtos>>(jsondata);
                return View(values);
            }
            return View();
        }
    }
}
