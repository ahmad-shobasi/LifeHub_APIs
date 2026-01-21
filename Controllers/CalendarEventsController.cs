using LifeHub_APIs.Dtos;
using LifeHub_APIs.services.CalendarEventsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LifeHub_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController(ICalendarEventsService service) : ControllerBase
    {
        private int GetUserId() =>
            Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CalendarEventResponseDto>>> GetEvents()
        {
            var events = await service.GetAllEventsAsync(GetUserId());
            var eventsDto = events.Select(e => new CalendarEventResponseDto
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToList();
            return Ok(eventsDto);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEventResponseDto?>> GetEventById(int id)
        {
            var Cevent = await service.GetEventByIdAsync(id ,GetUserId());
            var eventDto = Cevent is null ? null : new CalendarEventResponseDto
            {
                Id = Cevent.Id,
                Title = Cevent.Title,
                StartDate = Cevent.StartDate,
                EndDate = Cevent.EndDate
            };
            return eventDto is null ? NotFound() : Ok(eventDto);
        }

        [Authorize]
        [HttpPost("create-event")]
        public async Task<ActionResult<CalendarEventResponseDto>> CreateNewEvent(CalendarEventRequestDto request)
        {
            var craetedEvent = await service.CreateEventAsync(request, GetUserId());
            var createdEventDto = craetedEvent is null ? null : new CalendarEventResponseDto
            {
                Id = craetedEvent.Id,
                Title = craetedEvent.Title,
                StartDate = craetedEvent.StartDate,
                EndDate = craetedEvent.EndDate
            };
            return Ok(createdEventDto);
        }

        [Authorize]
        [HttpPut("update-event/{id}")]
        public async Task<ActionResult<CalendarEventResponseDto>> UpdateEvent(int id, CalendarEventRequestDto request)
        {
            var updatedEvent = await service.UpdateEventAsync(id, request, GetUserId());
            var updatedEventDto = updatedEvent is null ? null : new CalendarEventResponseDto
            {
                Id = updatedEvent.Id,
                Title = updatedEvent.Title,
                StartDate = updatedEvent.StartDate,
                EndDate = updatedEvent.EndDate
            };
            return updatedEventDto is null ? NotFound() : Ok(updatedEventDto);
        }
    }
}
