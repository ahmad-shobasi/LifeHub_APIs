using LifeHub_APIs.Models.DailyTasks;

namespace LifeHub_APIs.Dtos
{
    public class TaskRequestDto
    {
        public required string Title { get; set; }
        public string Description { get; set; } = String.Empty;
        public PriorityFlag priority { get; set; } = PriorityFlag.Done;
    }
}
