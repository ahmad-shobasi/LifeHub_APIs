using LifeHub_APIs.Models.DailyTasks;

namespace LifeHub_APIs.Dtos
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PriorityFlag Proirity { get; set; }
    }
}
