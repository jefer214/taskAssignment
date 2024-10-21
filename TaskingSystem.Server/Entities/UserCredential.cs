namespace TaskingSystem.Server.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserCredential : IValidatableObject
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = [];
            string messageRequired = "Field is required";

            if (string.IsNullOrEmpty(Email))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(Email)]));
            }
            if (string.IsNullOrEmpty(Password))
            {
                errors.Add(new ValidationResult(messageRequired, [nameof(Password)]));
            }

            return errors;
        }
    }
}
