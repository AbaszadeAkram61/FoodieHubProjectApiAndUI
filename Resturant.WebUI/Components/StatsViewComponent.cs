using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Resturant.WebUI.Components
{
    public class StatsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StatsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:44332/api/Static/ProductCount");
            var json=await responsemessage.Content.ReadAsStringAsync();
            ViewBag.productcount = json;


            var client1 = _httpClientFactory.CreateClient();
            var responsemessage1 = await client1.GetAsync("https://localhost:44332/api/Static/ReservationCount");
            var json1 = await responsemessage1.Content.ReadAsStringAsync();
            ViewBag.reservationcount = json1;


            var client2 = _httpClientFactory.CreateClient();
            var responsemessage2 = await client2.GetAsync("https://localhost:44332/api/Static/ChefCount");
            var json2 = await responsemessage2.Content.ReadAsStringAsync();
            ViewBag.chefcount = json2;


            var client3 = _httpClientFactory.CreateClient();
            var responsemessage3 = await client3.GetAsync("https://localhost:44332/api/Static/TotalGuestCount");
            var json3 = await responsemessage3.Content.ReadAsStringAsync();
            ViewBag.totalguestcount = json3;


            return View();
        }
    }
}
