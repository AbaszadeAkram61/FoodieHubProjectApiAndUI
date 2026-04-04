using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.ChefDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly ApiContext _apicontext;
        private readonly IValidator<Chef> _validation;
        public ChefController(ApiContext apicontext, IValidator<Chef> validation)
        {
            _apicontext = apicontext;
            _validation = validation;
        }

        [HttpGet]

        public async Task<IActionResult> GetChefs()
        {
            var chefs =await _apicontext.Chefs.ToListAsync();
            return Ok(chefs);
        }

        [HttpGet("Id")]

        public async Task<IActionResult> GetChefsFromById(int id)
        {
            var chef = await _apicontext.Chefs.FirstOrDefaultAsync(x=>x.ChefId==id);
            return Ok(chef);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
        {
            var chef = new Chef
            {
                NameSurname=createChefDto.NameSurname,
                Description=createChefDto.Description,
                Title=createChefDto.Title,
                ImageUrl=createChefDto.ImageUrl
            };
            var validationresult = _validation.Validate(chef);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apicontext.Chefs.AddAsync(chef);
           await _apicontext.SaveChangesAsync();
           return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChef(int id,UpdateChefDto updateChefDto)
        {
           var chef=await _apicontext.Chefs.FirstOrDefaultAsync(x => x.ChefId == id);
            chef.Description = updateChefDto.Description;
            chef.Title = updateChefDto.Title;
            chef.NameSurname = updateChefDto.NameSurname;
            chef.ImageUrl = updateChefDto.ImageUrl;
            var validatinresult = _validation.Validate(chef);
            if (!validatinresult.IsValid)
            {
                return BadRequest(validatinresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apicontext.SaveChangesAsync();
            return Ok(chef);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChefAsync(int id)
        {
            var chef = await _apicontext.Chefs.FirstOrDefaultAsync(x => x.ChefId == id);
            _apicontext.Chefs.Remove(chef);
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }


    }
}
