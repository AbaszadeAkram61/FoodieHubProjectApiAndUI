using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.FeatureDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IValidator<Feature> _validator;

        public FeatureController(ApiContext apiContext, IValidator<Feature> validator)
        {
            _apiContext = apiContext;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeatures()
        {
          var features= await _apiContext.Features.ToListAsync();
            return Ok(features);

        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetFeatureById(int id)
        {
           var feature=await _apiContext.Features.FirstOrDefaultAsync(x => x.FeatureId == id);
            return Ok(feature);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var feature = new Feature
            {
               Description=createFeatureDto.Description,
               VideoUrl=createFeatureDto.VideoUrl,
               ImageUrl=createFeatureDto.ImageUrl,
               Title=createFeatureDto.Title,
               SubTitle=createFeatureDto.SubTitle
            };

            var validationresult = _validator.Validate(feature);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apiContext.Features.AddAsync(feature);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(int id,UpdateFeatureDto updateFeatureDto)
        {
          var feature= await _apiContext.Features.FirstOrDefaultAsync(x => x.FeatureId == id);
            feature.Title = updateFeatureDto.Title;
            feature.SubTitle = updateFeatureDto.SubTitle;
            feature.Description = updateFeatureDto.Description;
            feature.VideoUrl = updateFeatureDto.VideoUrl;
            feature.ImageUrl = updateFeatureDto.ImageUrl;

            var validationresult = _validator.Validate(feature);  
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(int id)
        {
           var feature=await _apiContext.Features.FirstOrDefaultAsync(x => x.FeatureId == id);
            _apiContext.Features.Remove(feature);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
