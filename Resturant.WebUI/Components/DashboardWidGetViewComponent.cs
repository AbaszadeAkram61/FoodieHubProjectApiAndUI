using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Resturant.WebUI.Components
{
    public class DashboardWidGetViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardWidGetViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int r1, r2, r3, r4;
            Random random = new Random();
            r1 = random.Next(1, 35);
            r2 = random.Next(1, 35);
            r3 = random.Next(1, 35);
            r4 = random.Next(1, 35);
            var client = _httpClientFactory.CreateClient();
            var responsemessage= await client.GetAsync("https://localhost:44332/api/Reservation/ReservationCount");
            var json=await responsemessage.Content.ReadAsStringAsync();
            ViewBag.v1 = json;
            ViewBag.r1 = r1;

            var client1 = _httpClientFactory.CreateClient();
            var responsemessage1 = await client1.GetAsync("https://localhost:44332/api/Reservation/CustomerCount");
            var json1 = await responsemessage1.Content.ReadAsStringAsync();
            ViewBag.v2 = json1;
            ViewBag.r2 = r2;

            var client2 = _httpClientFactory.CreateClient();
            var responsemessage2 = await client2.GetAsync("https://localhost:44332/api/Reservation/PendingReservation");
            var json2 = await responsemessage2.Content.ReadAsStringAsync();
            ViewBag.v3 = json2;
            ViewBag.r3 = r3;


            var client3 = _httpClientFactory.CreateClient();
            var responsemessage3 = await client3.GetAsync("https://localhost:44332/api/Reservation/ApprovedReservation");
            var json3 = await responsemessage3.Content.ReadAsStringAsync();
            ViewBag.v4 = json3;
            ViewBag.r4 = r4;
            return View();

        }
    }
}
