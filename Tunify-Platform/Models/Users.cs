namespace Tunify_Platform.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } // Ensure this property exists
        public string Email { get; set; }
        public string EmailAddress { get; set; }
        public string DateJoined { get; set; }
        public int SubscriptionId { get; set; }
    }
}