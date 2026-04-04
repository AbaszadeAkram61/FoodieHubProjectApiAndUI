using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.Service;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IValidator<Service> _validator;

        public ServiceController(IValidator<Service> validator, ApiContext apicontext)
        {
            _validator = validator;
            _apicontext = apicontext;
        }

        private readonly ApiContext _apicontext;


        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
           var services=await _apicontext.Services.ToListAsync();
            return Ok(services);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetServiceById(int id)
        {
           var service= await _apicontext.Services.FirstOrDefaultAsync(x => x.ServiceId == id);
            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            var service = new Service
            {
                Title=createServiceDto.Title,
                Description=createServiceDto.Description,
                IconUrl=createServiceDto.IconUrl
            };

            var validatorresult = _validator.Validate(service);
            if (!validatorresult.IsValid)
            {
                return BadRequest(validatorresult.Errors.Select(x => x.ErrorMessage));
            }
           await _apicontext.Services.AddAsync(service);
           await _apicontext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(int id,UpdateServiceDto updateServiceDto)
        {
           var service=await _apicontext.Services.FirstOrDefaultAsync(x => x.ServiceId == id);
            service.Title = updateServiceDto.Title;
            service.Description = updateServiceDto.Description;
            service.IconUrl = updateServiceDto.IconUrl;

            var validatorresult = _validator.Validate(service);
            if (!validatorresult.IsValid)
            {
                return BadRequest(validatorresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apicontext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteSerive(int id)
        {
           var serive=await _apicontext.Services.FirstOrDefaultAsync(x => x.ServiceId == id);
            _apicontext.Services.Remove(serive);
           await _apicontext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }

    }
}
