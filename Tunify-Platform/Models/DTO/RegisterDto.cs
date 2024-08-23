namespace Tunify_Platform.Models.DTO
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        // Additional properties, such as first name, last name, etc., can be added as needed
    }
}
