using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;

namespace Resturant.WebUI.Controllers
{
    public class TestimoniallsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimoniallsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetTestimonials()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44332/api/Testimonial");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDtos>>(json);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateTestimonialForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDtos createTestimonial)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createTestimonial);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage = await client.PostAsync("https://localhost:44332/api/Testimonial", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetTestimonials");
            }
            return RedirectToAction("CreateServiceForm");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/Testimonial?id=" + id);
            return RedirectToAction("GetTestimonials");
        }

        public async Task<IActionResult> UpdateTestimonialForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Testimonial/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateTestimonialDtos>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDtos updateTestimonial)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateTestimonial);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Testimonial?id=" + updateTestimonial.TestimonialId, content);
            return RedirectToAction("GetTestimonials");

        }
    }
}
