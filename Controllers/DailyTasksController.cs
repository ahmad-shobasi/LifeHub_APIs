using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models.DailyTasks;
using LifeHub_APIs.services.DailyTasksServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LifeHub_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyTasksController(IDailyTasksService service) : ControllerBase
    {
        private int GetUserId() =>
            Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DailyTask>>> GetTasks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tasks = await service.GetAllTasksAsync(GetUserId());
            return Ok(tasks);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyTask?>> GetTaskById(int id)
        {
            var task = await service.GetTaskByIdAsync(id, GetUserId());
            return task is null ? BadRequest("No task with givin Id.") : Ok(task);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DailyTask?>> CreateNewTask(TaskRequestDto request)
        {
            var createdTask = await service.CreateTaskAsync(request, GetUserId());
            return Ok(createdTask);
        }
    }
}
