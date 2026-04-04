using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.ContactDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApiContext _apicontext;
        private readonly IValidator<Contact> _validator;

        public ContactController(ApiContext apicontext, IValidator<Contact> validator)
        {
            _apicontext = apicontext;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
           var contacts= await _apicontext.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact=await _apicontext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var contact = new Contact
            {
                Address=createContactDto.Address,
                MapLocation=createContactDto.MapLocation,
                Email=createContactDto.Email,
                OpenHourse=createContactDto.OpenHourse,
                Phone=createContactDto.Phone
            };

            var validationresult = _validator.Validate(contact);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apicontext.Contacts.AddAsync(contact);
           await _apicontext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(int id,UpdateContactDto updateContactDto)
        {
           var contact=await _apicontext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
            contact.MapLocation = updateContactDto.MapLocation;
            contact.Email = updateContactDto.Email;
            contact.Address = updateContactDto.Address;
            contact.Phone = updateContactDto.Phone;
            contact.OpenHourse = updateContactDto.OpenHourse;

            var validationresult = _validator.Validate(contact);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apicontext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
          var contact=await  _apicontext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
            _apicontext.Contacts.Remove(contact);
            await _apicontext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }


    }
}
