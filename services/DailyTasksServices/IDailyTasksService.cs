using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models.DailyTasks;

namespace LifeHub_APIs.services.DailyTasksServices
{
    public interface IDailyTasksService
    {
        Task<List<DailyTask>> GetAllTasksAsync(int userId);
        Task<DailyTask?> GetTaskByIdAsync(int id, int userId);
        Task<DailyTask?> CreateTaskAsync(TaskRequestDto request, int userId);
        Task<DailyTask?> UpdateTaskAsync(int id, TaskRequestDto request, int userId);
        Task<DailyTask?> DeleteTaskAsync(int id, int userId);
    }
}
