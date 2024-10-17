namespace TaskingSystem.Server.Entities.Request
{
    public class UpdateUserRequest : RegisterUserRequest
    {
        public UpdateUserRequest(string firstName, string lastName, string email, string password, int roleId) : base(firstName, lastName, email, password, roleId)
        {
        }

        public int UserId { get; set; }
    }
}
