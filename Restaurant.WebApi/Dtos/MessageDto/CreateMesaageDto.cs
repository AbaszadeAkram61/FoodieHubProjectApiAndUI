namespace Restaurant.WebApi.Dtos.MessageDto
{
    public class CreateMesaageDto
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageDetails { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
        public string? ImageUrl { get; set; }
    }
}
