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
        public async Task<ActionResult<List<TaskResponseDto>>> GetTasks()
        {
            var tasks = await service.GetAllTasksAsync(GetUserId());
            var tasksDto = new List<TaskResponseDto>();
            foreach (var task in tasks) { 
                var newTask = new TaskResponseDto
                {
                    Id = task.id,
                    Title = task.Title,
                    Description = task.Description,
                    Proirity = task.priority
                };
                tasksDto.Add(newTask);
            }
            return Ok(tasksDto);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto?>> GetTaskById(int id)
        {
            var task = await service.GetTaskByIdAsync(id, GetUserId());
            return task is null ? BadRequest("No task with givin Id.") : Ok(task);
        }
        [Authorize]
        [HttpPost("create-task")]
        public async Task<ActionResult<TaskResponseDto?>> CreateNewTask(TaskRequestDto request)
        {
            var createdTask = await service.CreateTaskAsync(request, GetUserId());
            return Ok(createdTask);
        }
        [Authorize]
        [HttpPut("update-task/{id}")]
        public async Task<ActionResult<TaskResponseDto?>> UpdateTask(int id, TaskRequestDto request)
        {
            var updatedTask = await service.UpdateTaskAsync(id, request, GetUserId());
            return updatedTask is null ? BadRequest("No task with givin Id.") : Ok(updatedTask);
        }

    }
}
