using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;

namespace Resturant.WebUI.Components.AdminLayoutViewComponent
{
    public class NavMessageListViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NavMessageListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44332/api/Message/MessageListFalse");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageListFalseDto>>(jsondata);
                return View(values);
            }

            return View();
        }
    }
}
