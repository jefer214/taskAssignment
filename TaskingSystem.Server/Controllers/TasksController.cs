namespace TaskingSystem.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using TaskingSystem.Server.AuthorizationFilter;
    using TaskingSystem.Server.Entities;
    using TaskingSystem.Server.Repositories.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Create new Task
        /// </summary>
        /// <returns>Tasks Model</returns>
        [AuthorizationFilter(nameof(CreateTask))]
        [HttpPost, Route("CreateTask")]
        [ProducesResponseType(typeof(Models.Task), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            object result = await _taskRepository.CreateTask(request);

            if (result.GetType() == typeof(Task))
            { 
                return Ok(result); 
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <returns>Tasks Model</returns>
        [AuthorizationFilter(nameof(UpdateTask))]
        [HttpPut, Route("UpdateTask")]
        [ProducesResponseType(typeof(Models.Task), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateTask(UpdateTaskRequest request)
        {
            object result = await _taskRepository.UpdateTask(request);
            
            if (result.GetType() == typeof(Task))
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Delete Task by Id
        /// </summary>
        /// <returns> Task Model</returns>
        [AuthorizationFilter(nameof(DeleteTask))]
        [HttpDelete]
        [ProducesResponseType(typeof(Models.Task), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            object result = await _taskRepository.DeleteTask(id);
            
            if (result.GetType() == typeof(Task))
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Create new Task
        /// </summary>
        /// <returns>UsersInTask Model</returns>
        [AuthorizationFilter(nameof(AssignTask))]
        [HttpPost, Route("AssignTask")]
        [ProducesResponseType(typeof(Models.UsersInTask), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignTask(int taskId, int userId)
        {
            object result = await _taskRepository.AssignTask(taskId, userId);

            if (result.GetType() == typeof(Task))
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Task status update
        /// </summary>
        /// <returns>Tasks Model</returns>
        [AuthorizationFilter(nameof(TaskStatusUpdate))]
        [HttpPut, Route("TaskStatusUpdate")]
        [ProducesResponseType(typeof(Models.Task), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TaskStatusUpdate(int taskId, int stateId)
        {
            object result = await _taskRepository.TaskStatusUpdate(taskId, stateId);

            if (result.GetType() == typeof(Task))
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
