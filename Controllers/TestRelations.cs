using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models.CalendarEvents;
using LifeHub_APIs.Models.DailyTasks;
using LifeHub_APIs.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LifeHub_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRelations(ITestRelations service) : ControllerBase
    {
        private int GetUserId() =>
             Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        [Authorize]
        [HttpGet("get-user-id")]
        public async Task<ActionResult<int>> GetUserIdTest()
        {
            return Ok(GetUserId());
        }

        [Authorize]
        [HttpGet("get-user-tasks")]
        public async Task<ActionResult<List<TaskResponseDto>>> GetUserTasksTest()
        {
            var tasks = await service.GetUserTasksTest(GetUserId());
            return Ok(tasks);
        }

        [Authorize]
        [HttpGet("get-user-events")]
        public async Task<ActionResult<List<CalendarEventResponseDto>>> GetUserEventsTest()
        {
            var events = await service.GetUserEventsTest(GetUserId());
            return Ok(events);
        }
    }
}
