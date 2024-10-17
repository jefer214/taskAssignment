namespace TaskingSystem.Server.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTaskRequest : IValidatableObject
    {
        public CreateTaskRequest(string nameTask, string? description, int userId, int stateId)
        {
            NameTask = nameTask;
            Description = description;
            UserId = userId;
            StateId = stateId;
        }

        public string NameTask { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }

        public int StateId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = [];
            string messageRequired = "Field is required";

            if (string.IsNullOrEmpty(NameTask))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(NameTask)]));
            }
            if (UserId < 1)
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(UserId)]));
            }
            if (StateId < 1)
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(StateId)]));
            }

            return errors;
        }
    }
}
