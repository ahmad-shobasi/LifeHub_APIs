using LifeHub_APIs.Data;
using LifeHub_APIs.Models.CalendarEvents;
using LifeHub_APIs.Models.DailyTasks;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.services
{
    public class TestRelationsService(AppDbContext context): ITestRelations
    {
        public async Task<List<DailyTask>> GetUserTasksTest(int userId)
        {
            var tasks = await context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
            return tasks;
        }
        public async Task<List<CalendarEvent>> GetUserEventsTest(int userId)
        {
            var events = await context.CalendarEvents
                .Where(e => e.UserId == userId)
                .ToListAsync();
            return events;
        }
    }
}
