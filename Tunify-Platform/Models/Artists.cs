namespace Tunify_Platform.Models
{
    public class Artists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}