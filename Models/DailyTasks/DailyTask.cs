using LifeHub_APIs.Relations;

namespace LifeHub_APIs.Models.DailyTasks
{
    public class DailyTask
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = String.Empty;
        public PriorityFlag priority { get; set; } = PriorityFlag.Done;

    }
}
