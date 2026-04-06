using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.WebApi.Dtos.Service;
using Resturant.WebUI.Models.Dto;
using System;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Resturant.WebUI.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetReservations()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44332/api/Reservation");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultReservationDtos>>(json);
                return View(values);
            }
            return View();
        }

      
        [HttpGet]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44332/api/Reservation?id=" + id);
            return RedirectToAction("GetReservations");
        }

        public async Task<IActionResult> UpdateReservationForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44332/api/Reservation/Id?id=" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateReservationDtos>(json);
                return View(value);
            }
         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDtos updateReservationDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateReservationDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44332/api/Reservation?id=" + updateReservationDtos.ReservationId, content);
            return RedirectToAction("GetReservations");
            return View();
        }

        public IActionResult ReservationFormPartial()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RezervationDo(CreateReservationDtos createReservationDtos)
        {
            createReservationDtos.ReservationStatus = "Gözləmədə";
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createReservationDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage = await client.PostAsync("https://localhost:44332/api/Reservation", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return Content("OK"); 
            }
            return Content("Error");
        }

        public async Task<IActionResult> AcceptReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:44332/api/Reservation/Accept?id=" +id);
            return RedirectToAction("GetReservations");
        }

        public async Task<IActionResult> CancelReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:44332/api/Reservation/Cancel?id=" +id);
            return RedirectToAction("GetReservations");
        }

        public async Task<IActionResult> WaitReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:44332/api/Reservation/Wait?id=" +id);
            return RedirectToAction("GetReservations");
        }
    }
}
