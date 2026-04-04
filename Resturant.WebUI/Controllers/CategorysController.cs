using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.WebApi.Dtos.CategoryDto;
using Resturant.WebUI.Models.Dto;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.WebUI.Controllers
{
    public class CategorysController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CategorysController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {


            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Category");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsondatastring = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDtos>>(jsondatastring);
                return View(values);
            }
            return View();
           
        }

        [HttpGet]
        public IActionResult CreateCategoryForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44332/api/Category", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }
            return RedirectToAction("CreateCategoryForm");
        }

        
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await  client.DeleteAsync("https://localhost:44332/api/Category?id=" + id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategoryForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage=await client.GetAsync("https://localhost:44332/api/Category/Id?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondatastring =await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCategoryDtos>(jsondatastring);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDtos updateCategoryDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(updateCategoryDtos);
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            await client.PutAsync($"https://localhost:44332/api/Category/{updateCategoryDtos.CategoryId}" ,stringContent);
            return RedirectToAction("CategoryList");
        }

    }
}
