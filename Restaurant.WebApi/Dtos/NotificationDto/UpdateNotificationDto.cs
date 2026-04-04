namespace Restaurant.WebApi.Dtos.NotificationDto
{
    public class UpdateNotificationDto
    {
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsRead { get; set; }
    }
}
