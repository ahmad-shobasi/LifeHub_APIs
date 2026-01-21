namespace LifeHub_APIs.Models.CalendarEvents
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
