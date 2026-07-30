namespace GamersDock.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        // Navigation property to represent the relationship with Games
        public ICollection<Game>? Games { get; set; }
    }
}