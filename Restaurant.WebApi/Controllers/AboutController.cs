using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.AboutDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public AboutController(ApiContext apiContext, IValidator<About> validator)
        {
            _apiContext = apiContext;
            _validator = validator;
        }

        private readonly IValidator<About> _validator;

        [HttpGet]
        public async Task<IActionResult> GetAbouts()
        {
           var abouts=await _apiContext.Abouts.ToListAsync();
            return Ok(abouts);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetAboutById(int id)
        {
           var about=await _apiContext.Abouts.FirstOrDefaultAsync(x => x.AboutId == id);
            return Ok(about);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var about = new About
            {
                Title=createAboutDto.Title,
                ImageUrl=createAboutDto.ImageUrl,
                VideoCoverImageUrl=createAboutDto.VideoCoverImageUrl,
                VideoUrl=createAboutDto.VideoUrl,
                Description=createAboutDto.Description,
                ReservationNumber=createAboutDto.ReservationNumber
            };

            var validationresult = _validator.Validate(about);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.Abouts.AddAsync(about);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(int id,UpdateAboutDto updateAboutDto)
        {
           var about=await _apiContext.Abouts.FirstOrDefaultAsync(x => x.AboutId == id);
            about.Title = updateAboutDto.Title;
            about.ImageUrl = updateAboutDto.ImageUrl;
            about.VideoCoverImageUrl = updateAboutDto.VideoCoverImageUrl;
            about.VideoUrl = updateAboutDto.VideoUrl;
            about.Description = updateAboutDto.Description;
            about.ReservationNumber = updateAboutDto.ReservationNumber;

            var validationresult = _validator.Validate(about);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAboutAsync(int id)
        {
            var about = await _apiContext.Abouts.FirstOrDefaultAsync(x => x.AboutId == id);
            _apiContext.Abouts.Remove(about);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
