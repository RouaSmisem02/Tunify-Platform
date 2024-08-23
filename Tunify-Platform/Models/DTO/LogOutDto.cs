namespace Tunify_Platform.Models.DTO
{
    public class LogOutDto
    {
        public LoginDto user { get; set; }

        // Computed property for convenience
        public string Username => user?.Username;
    }
}
