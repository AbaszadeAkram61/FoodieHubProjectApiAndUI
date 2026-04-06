using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.WebApi.Dtos.ContactDto;
using Resturant.WebUI.Models.Dto;
using System.Text;

namespace Resturant.WebUI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessagesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetMessages()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44332/api/Message");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDtos>>(json);
                return View(values);
            }
            return View();
        }

       
        [HttpGet]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/Message?id=" + id);
            return RedirectToAction("GetMessages");
        }

        public async Task<IActionResult> UpdateMessageForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Message/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateMessageDtos>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDtos updateMessageDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateMessageDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Message?id=" + updateMessageDtos.MessageId, content);
            return RedirectToAction("GetMessages");
            return View();
        }

        public async Task<IActionResult> IncomingMessageForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Message/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultMessageDtos>(json);
                return View(value);
            }
            return View();
        }


        public IActionResult SendMessageForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDtos createMessageDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createMessageDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage = await client.PostAsync("https://localhost:44332/api/Message/", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return Content("OK");
            }
            return Content("Error");
        }


        [HttpPost]
        public async Task<IActionResult> SendReply(MessageResponseMailDtos model)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync("https://localhost:44332/api/Message/SendReply", content);

            return RedirectToAction("GetMessages");
        }
    }
}
