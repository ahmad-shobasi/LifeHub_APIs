namespace LifeHub_APIs.Dtos
{
    public class CalendarEventRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
