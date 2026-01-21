using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models.CalendarEvents;

namespace LifeHub_APIs.services.CalendarEventsServices
{
    public interface ICalendarEventsService
    {
        Task<List<CalendarEvent>> GetAllEventsAsync(int userId);
        Task<CalendarEvent?> GetEventByIdAsync(int id, int userId);
        Task<CalendarEvent?> CreateEventAsync(CalendarEventRequestDto request, int userId);
        Task<CalendarEvent?> UpdateEventAsync(int id, CalendarEventRequestDto request, int userId);
        Task<CalendarEvent?> DeleteEventAsync(int id, int userId);
    }
}
