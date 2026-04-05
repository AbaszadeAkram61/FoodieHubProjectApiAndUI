using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Models.Context;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public StaticController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("ProductCount")]
        public async Task<IActionResult> ProductCount()
        {
            var productsCount=await _apiContext.Products.CountAsync();
            return Ok(productsCount);
        }

        [HttpGet("ReservationCount")]
        public async Task<IActionResult> ReservationCount()
        {
            var reservationCount =await _apiContext.Reservations.CountAsync();
            return Ok(reservationCount);

        }

        [HttpGet("ChefCount")]
        public async Task<IActionResult> ChefCount()
        {
           var chefCount=await _apiContext.Chefs.CountAsync();
            return Ok(chefCount);
        }

        [HttpGet("TotalGuestCount")]
        public async Task<IActionResult> TotalGuestCount()
        {
           var sumGuestCount=await _apiContext.Reservations.SumAsync(x => x.CountPeople);
            return Ok(sumGuestCount);
        }
    }
}
