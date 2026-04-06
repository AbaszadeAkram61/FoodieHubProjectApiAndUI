using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.ReservationDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IValidator<Reservation> _validator;

        public ReservationController(IValidator<Reservation> validator, ApiContext apiContext)
        {
            _validator = validator;
            _apiContext = apiContext;
        }

        private readonly ApiContext _apiContext;

        [HttpGet]
        public async Task<IActionResult> GetReservation()
        {
           var reservations=await _apiContext.Reservations.ToListAsync();
            return Ok(reservations);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetReservationById(int id)
        {
           var reservation=await _apiContext.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            return Ok(reservation);

        }

        [HttpPost]
        public async Task<IActionResult> Createreservation(CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation
            {
                NameSurname=createReservationDto.NameSurname,
                Email=createReservationDto.Email,
                PhoneNumber=createReservationDto.PhoneNumber,
                ReservationDate=createReservationDto.ReservationDate,
                ReservationTime=createReservationDto.ReservationTime,
                CountPeople=createReservationDto.CountPeople,
                Message=createReservationDto.Message,
                ReservationStatus=createReservationDto.ReservationStatus
            };

            var validationresult = _validator.Validate(reservation);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.Reservations.AddAsync(reservation);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(int id,UpdateReservationDto updateReservationDto)
        {
           var reservation=await _apiContext.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            reservation.NameSurname = updateReservationDto.NameSurname;
            reservation.Email = updateReservationDto.Email;
            reservation.PhoneNumber = updateReservationDto.PhoneNumber;
            reservation.ReservationDate = updateReservationDto.ReservationDate;
            reservation.ReservationTime = updateReservationDto.ReservationTime;
            reservation.CountPeople = updateReservationDto.CountPeople;
            reservation.Message = updateReservationDto.Message;
            reservation.ReservationStatus = updateReservationDto.ReservationStatus;

            var validationresult = _validator.Validate(reservation);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirili");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(int id)
        {
           var reservation=await _apiContext.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            _apiContext.Reservations.Remove(reservation);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }


        [HttpGet("ReservationCount")]
        public async Task<IActionResult> GetCustomerCountAsync()
        {
            var value = await _apiContext.Reservations.CountAsync();
            return Ok(value);
        }

        [HttpGet("CustomerCount")]
        public async Task<IActionResult> PendingReservation()
        {
            var value = await _apiContext.Reservations.SumAsync(x=>x.CountPeople);
            return Ok(value);
        }


        [HttpGet("PendingReservation")]
        public async Task<IActionResult> GetApprovedReservation()
        {
            var value = await _apiContext.Reservations.Where(x=>x.ReservationStatus== "Gözləmədə").CountAsync();
            return Ok(value);
        }


        [HttpGet("ApprovedReservation")]
        public async Task<IActionResult> GetReservationCountAsync()
        {
            var value = await _apiContext.Reservations.Where(x => x.ReservationStatus == "Təsdiqləndi").CountAsync();
            return Ok(value);
        }


        [HttpGet("Cancel")]
        public async Task<IActionResult> CancelReservation(int id)
        {
           var reservation=await _apiContext.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            reservation.ReservationStatus = "Ləğv edildi";
            await _apiContext.SaveChangesAsync();
            return Ok(reservation);
        }

        [HttpGet("Wait")]
        public async Task<IActionResult> WaitReservation(int id)
        {
            var reservation = await _apiContext.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            reservation.ReservationStatus = "Gözləmədə";
            await _apiContext.SaveChangesAsync();
            return Ok(reservation);
        }

        [HttpGet("Accept")]
        public async Task<IActionResult> AcceptReservation(int id)
        {
            var reservation = await _apiContext.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            reservation.ReservationStatus = "Təsdiqləndi";
            await _apiContext.SaveChangesAsync();
            return Ok(reservation);
        }


    }
}
