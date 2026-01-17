using LifeHub_APIs.Data;
using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models.DailyTasks;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.services.DailyTasksServices
{
    public class DailyTasksService(AppDbContext context) : IDailyTasksService
    {
        public async Task<List<DailyTask>> GetAllTasksAsync(int userId)
        {
            var tasks = await context.Tasks
                .Where(t=> t.UserId == userId)
                .ToListAsync();
            return tasks;
        }
        public async Task<DailyTask?> GetTaskByIdAsync(int id, int userId)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t=> t.id == id && t.UserId == userId);
            return task is null ? null : task;
        }
        public async Task<DailyTask?> CreateTaskAsync(TaskRequestDto request, int userId)
        {
            var task = new DailyTask
            {
                Title = request.Title,
                Description = request.Description,
                priority = request.priority,
                UserId = userId
            };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }
        public async Task<DailyTask?> UpdateTaskAsync(int id, TaskRequestDto request, int userId)
        {
            throw new Exception();
        }
        public async Task<DailyTask?> DeleteTaskAsync(int id, int userId)
        {
            throw new Exception();
        }
    }
}
