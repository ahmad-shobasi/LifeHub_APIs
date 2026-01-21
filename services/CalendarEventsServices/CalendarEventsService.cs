using LifeHub_APIs.Data;
using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models.CalendarEvents;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.services.CalendarEventsServices
{
    public class CalendarEventsService(AppDbContext context): ICalendarEventsService
    {
        public async Task<List<CalendarEvent>> GetAllEventsAsync(int userId)
        {
            var events = await context.CalendarEvents
                .Where(e => e.UserId == userId)
                .ToListAsync();
            return events;
        }
        public async Task<CalendarEvent?> GetEventByIdAsync(int id, int userId)
        {
            var Cevent = await context.CalendarEvents
                .Where(e=> e.UserId == userId && e.Id == id)
                .FirstOrDefaultAsync();
            return Cevent is null ? null : Cevent;
        }

        public async Task<CalendarEvent?> CreateEventAsync(CalendarEventRequestDto request, int userId)
        {
            var Cevent = new CalendarEvent
            {
                Title = request.Title,
                StartDate = request.StartTime,
                EndDate = request.EndTime,
                UserId = userId
            };
            context.CalendarEvents.Add(Cevent);
            await context.SaveChangesAsync();
            return Cevent;
        }

        public async Task<CalendarEvent?> UpdateEventAsync(int id, CalendarEventRequestDto request, int userId)
        {
            var Cevent = await context.CalendarEvents
                .Where(e => e.UserId == userId && e.Id == id)
                .FirstOrDefaultAsync();
            if (Cevent is null)
                return null;
            Cevent.Title = request.Title;
            Cevent.StartDate = request.StartTime;
            Cevent.EndDate = request.EndTime;
            await context.SaveChangesAsync();
            return Cevent;
        }

         public async Task<CalendarEvent?> DeleteEventAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

       
    }
}
