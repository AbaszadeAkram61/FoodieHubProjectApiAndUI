using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.CategoryDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApiContext _apicontext;
        private readonly IValidator<Category> _validator;
        public CategoryController(ApiContext apicontext, IValidator<Category> validator)
        {
            _apicontext = apicontext;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _apicontext.Categories.ToListAsync();

            return Ok(categories);
        }


        [HttpGet("Id")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var category = await _apicontext.Categories.FirstOrDefaultAsync(x=>x.CategoryId==id);

            return Ok(category);
        }


        [HttpPost()]

        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                CategoryName=createCategoryDto.CategoryName
            };

            var valitationresult = _validator.Validate(category);
            if (!valitationresult.IsValid)
            {
                return BadRequest(valitationresult.Errors.Select(x => x.ErrorMessage));
            }

           var categories= await _apicontext.Categories.AddAsync(category);
           await _apicontext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryDto updateCategoryDto)
        {
           var category=await _apicontext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            category.CategoryName = updateCategoryDto.CategoryName;

            var valitationresult = _validator.Validate(category);
            if (!valitationresult.IsValid)
            {
                return BadRequest(valitationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apicontext.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete()]

        public async Task<IActionResult> DeleteCategory(int id)
        {
           var category=await _apicontext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            _apicontext.Categories.Remove(category);
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
