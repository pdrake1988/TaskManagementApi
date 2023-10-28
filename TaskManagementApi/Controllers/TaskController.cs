using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskRepository.GetAll());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Task? task = _taskRepository.GetById(id);
            if (task is not null)
            {
                return Ok(task);
            }
            return NotFound();
        }
        
        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                Task task = new Task(taskModel.Name, taskModel.Description, taskModel.CategoryId, taskModel.DueDate, taskModel.UserId);
                _taskRepository.Add(task);
                return Ok();
            }
            return BadRequest();
        }
    }
}
