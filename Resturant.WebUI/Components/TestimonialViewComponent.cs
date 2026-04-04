using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Threading.Tasks;

namespace Resturant.WebUI.Components
{
    public class TestimonialViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Testimonial");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsondatastring = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDtos>>(jsondatastring);
                return View(values);
            }
            return View();
        }
    }
}
