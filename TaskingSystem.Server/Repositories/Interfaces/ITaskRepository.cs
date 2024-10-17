namespace TaskingSystem.Server.Repositories.Interfaces
{
    using TaskingSystem.Server.Entities;

    public interface ITaskRepository
    {
        /// <summary>
        /// Method to Create Tasks 
        /// </summary>
        /// <returns>Tasks Model or ErrorresponseModel</returns>
        Task<object> CreateTask(CreateTaskRequest taskRequest);

        /// <summary>
        /// Method to Update Tasks 
        /// </summary>
        /// <returns>Tasks Model or ErrorresponseModel</returns>
        Task<object> UpdateTask(UpdateTaskRequest taskRequest);

        /// <summary>
        /// Method to delete Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<object> DeleteTask(int id);

        /// <summary>
        /// Method to assign Task to user
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<object> AssignTask(int taskId, int userId);

        /// <summary>
        /// Method to task status update
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stateId"></param>
        /// <returns>Task model</returns>
        Task<object> TaskStatusUpdate(int taskId, int stateId);
    }
}
