namespace Restaurant.WebApi.Models.Entities
{
    public class Message
    {
        public int MessageId {  get; set; }
        public string NameSurname {  get; set; }
        public string Email {  get; set; }
        public string Subject {  get; set; }
        public string MessageDetails {  get; set; }
        public DateTime SendDate {  get; set; }
<<<<<<< HEAD
        public bool IsRead {  get; set; }
=======
        public bool? IsRead {  get; set; }
>>>>>>> 13c35dd (Bu gun Dasboard Da deyisiklikler etdim)

        public string? ImageUrl {  get; set; }
    }
}
