using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.TestimonialDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ApiContext _apicontext;

        public TestimonialController(ApiContext apicontext, IValidator<Testimonial> validator)
        {
            _apicontext = apicontext;
            _validator = validator;
        }

        private readonly IValidator<Testimonial> _validator;


        [HttpGet]
        public async Task<IActionResult> GetTestimonialsAsync()
        {
            var testimonials = await _apicontext.Testimonials.ToListAsync();
            return Ok(testimonials);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetTestimonialById(int id)
        {
            var testimonail =await _apicontext.Testimonials.FirstOrDefaultAsync(x => x.TestimonialId == id);
            return Ok(testimonail);
        }

        [HttpPost]
        public async Task<IActionResult> Createtestimonail(CreateTestimonialDto createTestimonialDto)
        {
            var testimonal = new Testimonial
            {
                NameSurname=createTestimonialDto.NameSurname,
                Title=createTestimonialDto.Title,
                Comment=createTestimonialDto.Comment,
                ImageUrl=createTestimonialDto.ImageUrl
            };

            var validationresult = _validator.Validate(testimonal);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apicontext.Testimonials.AddAsync(testimonal);
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Updatetestimonial(int id,UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonal =await _apicontext.Testimonials.FirstOrDefaultAsync(x => x.TestimonialId == id);
            testimonal.NameSurname = updateTestimonialDto.NameSurname;
            testimonal.Comment = updateTestimonialDto.Comment;
            testimonal.Title = updateTestimonialDto.Title;
            testimonal.ImageUrl = updateTestimonialDto.ImageUrl;

            var validationresult = _validator.Validate(testimonal);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }
        [HttpDelete]
        public async Task<IActionResult> Deletetestimonail(int id)
        {
           var testimonail=await _apicontext.Testimonials.FirstOrDefaultAsync(x => x.TestimonialId == id);
            _apicontext.Testimonials.Remove(testimonail);
           await _apicontext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
