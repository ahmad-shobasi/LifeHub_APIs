using LifeHub_APIs.Models.CalendarEvents;
using LifeHub_APIs.Models.DailyTasks;

namespace LifeHub_APIs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<DailyTask> Tasks { get; set; } = new List<DailyTask>();
        public ICollection<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();
    }
}
