using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.EventDto;
using Restaurant.WebApi.Dtos.FeatureDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IValidator<Event> _validator;

        public EventController(IValidator<Event> validator, ApiContext apicontext)
        {
            _validator = validator;
            _apicontext = apicontext;
        }

        private readonly ApiContext _apicontext;

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events =await _apicontext.Events.ToListAsync();
            return Ok(events);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetFeatureById(int id)
        {
            var _event= await _apicontext.Events.FirstOrDefaultAsync(x => x.EventId == id);
            return Ok(_event);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateEventDto createEventDto)
        {
            var _event = new Event
            {
                Title=createEventDto.Title,
                Description=createEventDto.Description,
                ImageUrl=createEventDto.ImageUrl,
                Status=createEventDto.Status,
                Price=createEventDto.Price
            };

            var validationresult = _validator.Validate(_event);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apicontext.Events.AddAsync(_event);
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(int id, UpdateEventDto updateEventDto)
        {
            var _event = await _apicontext.Events.FirstOrDefaultAsync(x => x.EventId == id);
            _event.Title = updateEventDto.Title;
            _event.Price = updateEventDto.Price;
            _event.Description = updateEventDto.Description;
            _event.Status = updateEventDto.Status;
            _event.ImageUrl = updateEventDto.ImageUrl;

            var validationresult = _validator.Validate(_event);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var _event = await _apicontext.Events.FirstOrDefaultAsync(x => x.EventId == id);
            _apicontext.Events.Remove(_event);
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }

    }
}
