using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.Notification;
using Restaurant.WebApi.Dtos.NotificationDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly IValidator<Notification> _validator;

        public NotificationController(IValidator<Notification> validator, ApiContext apiContext)
        {
            _validator = validator;
            _apiContext = apiContext;
        }

        private readonly ApiContext _apiContext;

        [HttpGet]
        public async Task<IActionResult> GetNotificationsAsync()
        {
            var notifications = await _apiContext.Notifications.ToListAsync();
            return Ok(notifications);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _apiContext.Notifications.FirstOrDefaultAsync(x => x.NotificationId == id);
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var notification = new Notification
            {
                Description = createNotificationDto.Description,
                IconUrl = createNotificationDto.IconUrl,
                NotificationDate = createNotificationDto.NotificationDate,
                IsRead = createNotificationDto.IsRead
            };

            var validationresult = _validator.Validate(notification);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.Notifications.AddAsync(notification);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat elave olundu");

        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotification(int id, UpdateNotificationDto updateNotificationDto)
        {
            var notification = await _apiContext.Notifications.FirstOrDefaultAsync(x => x.NotificationId == id);
            notification.Description = updateNotificationDto.Description;
            notification.IconUrl = updateNotificationDto.IconUrl;
            notification.NotificationDate = updateNotificationDto.NotificationDate;
            notification.IsRead = updateNotificationDto.IsRead;

            var validationresult = _validator.Validate(notification);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _apiContext.Notifications.FirstOrDefaultAsync(x => x.NotificationId == id);
            _apiContext.Notifications.Remove(notification);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
