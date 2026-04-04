using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.PhotoDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public PhotoController(ApiContext apiContext, IValidator<Photo> validator)
        {
            _apiContext = apiContext;
            _validator = validator;
        }

        private readonly IValidator<Photo> _validator;

        [HttpGet]
        public async Task<IActionResult> GetPhotos()
        {
            var Photos = await _apiContext.Photos.ToListAsync();
            return Ok(Photos);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetPhotoById(int id)
        {
            var Photo = await _apiContext.Photos.FirstOrDefaultAsync(x => x.PhotoId == id);
            return Ok(Photo);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhoto(CreatePhotoDto createPhotoDto)
        {
            var Photo = new Photo
            {
                Title = createPhotoDto.Title,
                PhotoUrl = createPhotoDto.PhotoUrl,
               
            };

            var validationresult = _validator.Validate(Photo);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.Photos.AddAsync(Photo);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePhoto(int id, UpdatePhotoDto updatePhotoDto)
        {
            var Photo = await _apiContext.Photos.FirstOrDefaultAsync(x => x.PhotoId == id);
            Photo.Title = updatePhotoDto.Title;
            Photo.PhotoUrl = updatePhotoDto.PhotoUrl;
        

            var validationresult = _validator.Validate(Photo);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhotoAsync(int id)
        {
            var Photo = await _apiContext.Photos.FirstOrDefaultAsync(x => x.PhotoId == id);
            _apiContext.Photos.Remove(Photo);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
