namespace TaskingSystem.Server.Entities.Request
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserRequest : IValidatableObject
    {
        public RegisterUserRequest(string firstName, string lastName, string email, string password, int roleId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = [];
            string messageRequired = "Field is required";

            if (string.IsNullOrEmpty(FirstName))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(FirstName)]));
            }
            if (string.IsNullOrEmpty(LastName))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(LastName)]));
            }
            if (string.IsNullOrEmpty(Email))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(Email)]));
            }
            if (string.IsNullOrEmpty(Password))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(Password)]));
            }
            if (RoleId < 1)
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(RoleId)]));
            }

            return errors;
        }
    }
}
