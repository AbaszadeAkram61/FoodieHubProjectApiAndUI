namespace Restaurant.WebApi.Dtos.ReservationDto
{
    public class UpdateReservationDto
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public int CountPeople { get; set; }
        public string Message { get; set; }
        public string ReservationStatus { get; set; }
    }
}
