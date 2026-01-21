using LifeHub_APIs.Models.CalendarEvents;
using LifeHub_APIs.Models.DailyTasks;

namespace LifeHub_APIs.services
{
    public interface ITestRelations
    {
        Task<List<DailyTask>> GetUserTasksTest(int userId);
        Task<List<CalendarEvent>> GetUserEventsTest(int userId);
    }
}
