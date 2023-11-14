using System.Security.Claims;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using Task = Core.Entities.Task;

namespace TaskManagementApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<Account> _userManager;
        public TaskController(ITaskRepository taskRepository, UserManager<Account> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTasks()
        {
            Account user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user != null)
            {
                List<Task> tasks = await _taskRepository.GetAll();
                return Ok(tasks.Where(t => t.UserId == user.Id));
            }
            return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Task? task = await _taskRepository.GetById(id);
            if (task != null)
            {
                return Ok(task);
            }
            return NotFound();
        }
        
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] TaskModel taskModel)
        {
            Account? user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid && user != null)
            {
                Task task = new Task(taskModel.Name, taskModel.Description, taskModel.CategoryId, taskModel.DueDate, user.Id);
                _taskRepository.Add(task);
                return Ok();
            }
            return BadRequest();
        }
    }
}
