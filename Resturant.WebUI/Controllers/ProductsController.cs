using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant.WebUI.Models.Dto;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetProducts()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:44332/api/Product");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json =await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(json);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> CreateProductForm()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:44332/api/Category");
            var json = await response.Content.ReadAsStringAsync();

            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDtos>>(json);

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDtos createProductDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createProductDtos);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var messagerespons=await client.PostAsync("https://localhost:44332/api/Product", stringContent);
            if (messagerespons.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("CreateProductForm");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response= await client.DeleteAsync("https://localhost:44332/api/Product?id=" + id);

            return RedirectToAction("GetProducts");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage= await client.GetAsync("https://localhost:44332/api/Product/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json=await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateProductDtos>(json);


                var response = await client.GetAsync("https://localhost:44332/api/Category");
                var jsondata = await response.Content.ReadAsStringAsync();

                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDtos>>(jsondata);

                ViewBag.Categories = categories;


                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDtos updateProductDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateProductDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync($"https://localhost:44332/api/Product/{updateProductDtos.ProductId}", content);
            return RedirectToAction("GetProducts");


        }


    }
}
