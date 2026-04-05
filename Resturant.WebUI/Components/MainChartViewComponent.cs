using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Resturant.WebUI.Components
{
    public class MainChartViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MainChartViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
           var responsemessage=await client.GetAsync("https://localhost:44332/api/Reservation/ReservationCount");
           var jsonTotalReservation=await responsemessage.Content.ReadAsStringAsync();
           ViewBag.totalReservation = jsonTotalReservation;


            var client1 = _httpClientFactory.CreateClient();
            var responsemessage1 = await client1.GetAsync("https://localhost:44332/api/Reservation/CustomerCount");
            var jsonCustomerCount = await responsemessage1.Content.ReadAsStringAsync();
            ViewBag.customerCount = jsonCustomerCount;


            var client2 = _httpClientFactory.CreateClient();
            var responsemessage2 = await client2.GetAsync("https://localhost:44332/api/Reservation/PendingReservation");
            var jsonPending = await responsemessage2.Content.ReadAsStringAsync();
            ViewBag.pending = jsonPending;

            var client3 = _httpClientFactory.CreateClient();
            var responsemessage3 = await client3.GetAsync("https://localhost:44332/api/Reservation/ApprovedReservation");
            var jsonApproved = await responsemessage3.Content.ReadAsStringAsync();
            ViewBag.approved = jsonApproved;

            return View();
        }
    }
}
