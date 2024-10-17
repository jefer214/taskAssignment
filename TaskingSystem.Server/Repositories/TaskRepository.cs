using TaskingSystem.Server.Repositories.Interfaces;

namespace TaskingSystem.Server.Repositories
{
    using Azure.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using TaskingSystem.Server.Entities;
    using TaskingSystem.Server.Entities.Request;
    using TaskingSystem.Server.Models;

    public class TaskRepository : ITaskRepository
    {
        private readonly TaskAssignmentContext _context;
        public TaskRepository(TaskAssignmentContext context)
        {
            _context = context;
        }

        ///<summary>
        /// Create new task in the database
        ///</summary>
        ///<param name="taskRequest"> taskRequest </param>
        ///<returns> Task Model, ErrorResponsesResponseModel</returns>

        public async Task<object> CreateTask(CreateTaskRequest request)
        {
            try
            {
                var validateUser = _context.Users.FirstOrDefault(u => u.Id == request.UserId);
                var ValidateStatusTask = _context.TaskStatuses.FirstOrDefault(s => s.Id == request.StateId);

                if (validateUser is null
                    || ValidateStatusTask is null)
                {
                    string field = validateUser is null ? "User" : "Task status";
                    return new NotFoundObjectResult($"{field} no found");
                }

                var ValidateTask = _context.Tasks.FirstOrDefault(t => t.UserId == request.UserId
                                                                    && t.NameTask.Equals(request.NameTask));

                if (ValidateTask is not null)
                {
                    return new NotFoundObjectResult($"There is already a task {request.NameTask} associated with the user");
                }

                Models.Task user = new()
                {
                    NameTask = request.NameTask,
                    Description = request.Description,
                    UserId = request.UserId,
                    StateId = request.StateId
                };

                var newUser = await _context.Tasks.AddAsync(user).ConfigureAwait(false);
                await _context.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Task by Id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<object> DeleteTask(int id)
        {
            try
            {
                Models.Task? validateTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

                if (validateTask is null)
                {
                    return new NotFoundObjectResult("Task no found");
                }

                _context.Tasks.Remove(validateTask);
                await _context.SaveChangesAsync();

                return validateTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<object> UpdateTask(UpdateTaskRequest request)
        {
            try
            {
                Models.Task? task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id).ConfigureAwait(false);

                if (task is null)
                {
                    return new NotFoundObjectResult("Task no found");
                }

                task.NameTask = request.NameTask;
                task.Description = request.Description;
                task.UserId = request.UserId;
                task.StateId = request.StateId;

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Assign Task to User
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <returns>UsersInTask Model</returns>
        public async Task<object> AssignTask(int taskId, int userId)
        {
            var validateUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            var validateTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId).ConfigureAwait(false);

            if (validateUser is null
                || validateTask is null)
            {
                string field = validateUser is null ? "User" : "Task";
                return new NotFoundObjectResult($"{field} no found");
            }

            var validateUserInTask = await _context.UsersInTasks.FirstOrDefaultAsync(u => u.TaskId == taskId
                                                                                  && u.UserId == userId).ConfigureAwait(false);
            if (validateUserInTask is null)
            {
                return "The task is already associated with this user";
            }

            UsersInTask userInTasks = new()
            {
                UserId = userId,
                TaskId = taskId
            };

            await _context.UsersInTasks.AddAsync(userInTasks).ConfigureAwait(false);
            await _context.SaveChangesAsync();

            return userInTasks;
        }

        /// <summary>
        /// Task status update
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        /// <returns>Task Model</returns>
        public async Task<object> TaskStatusUpdate(int taskId, int stateId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId).ConfigureAwait(false);
            var validateStatusTask = _context.TaskStatuses.FirstOrDefault(s => s.Id == stateId);

            if (task is null
                  || validateStatusTask is null)
            {
                string field = task is null ? "Task" : "Task status";
                return new NotFoundObjectResult($"{field} no found");
            }

            task.StateId = stateId;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
    }
}
