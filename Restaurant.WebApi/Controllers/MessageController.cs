using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.MessageDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IValidator<Message> _validator;

        public MessageController(ApiContext apiContext, IValidator<Message> validator)
        {
            _apiContext = apiContext;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
           var messages=await _apiContext.Messages.ToListAsync();
            return Ok(messages);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetMessagesById(int id)
        {
           var message= await _apiContext.Messages.FirstOrDefaultAsync(x => x.MessageId == id);
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMesaageDto createMesaageDto)
        {
            var message = new Message
            {
                NameSurname=createMesaageDto.NameSurname,
                Email=createMesaageDto.Email,
                Subject=createMesaageDto.Subject,
                MessageDetails=createMesaageDto.MessageDetails,
                SendDate=createMesaageDto.SendDate,
                IsRead=createMesaageDto.IsRead,
                ImageUrl=createMesaageDto.ImageUrl
            };

            var validationresult = _validator.Validate(message);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apiContext.Messages.AddAsync(message);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage(int id,UpdateMessageDto updateMessageDto)
        {
           var message=await _apiContext.Messages.FirstOrDefaultAsync(x => x.MessageId == id);
            message.NameSurname = updateMessageDto.NameSurname;
            message.Email = updateMessageDto.Email;
            message.Subject = updateMessageDto.Subject;
            message.MessageDetails = updateMessageDto.MessageDetails;
            message.SendDate = updateMessageDto.SendDate;
            message.IsRead = updateMessageDto.IsRead;
            message.ImageUrl = updateMessageDto.ImageUrl;

            var validationresult = _validator.Validate(message);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessageAsync(int id)
        {
            var message = await _apiContext.Messages.FirstOrDefaultAsync(x => x.MessageId == id);
            _apiContext.Messages.Remove(message);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }

        [HttpGet("MessageListFalse")]
        public async Task<IActionResult> GetMessageFalseIsRead()
        {
            var messages = await _apiContext.Messages.Where(x => x.IsRead == false).ToListAsync();
            return Ok(messages);
        }
    }
}
