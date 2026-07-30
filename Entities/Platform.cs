namespace GamersDock.Entities
{
    public class Platform
    {
        public int PlatformId { get; set; }
        public string? Name { get; set; }

        // Navigation property to represent the relationship with Games
        public ICollection<Games>? Games { get; set; }
    }
}