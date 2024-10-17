namespace TaskingSystem.Server.Entities
{
    public class UpdateTaskRequest : CreateTaskRequest
    {
        public UpdateTaskRequest(string nameTask, string? description, int userId, int stateId) : base(nameTask, description, userId, stateId)
        {
        }

        public int Id { get; set; }
    }
}
