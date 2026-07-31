namespace GamersDock.Entities
{
    public class LibraryEntry
    {
        public int LibraryEntryId { get; set; }
        public int GameId { get; set; }
        public Status Status { get; set; }
        public float? Rating { get; set; }
        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastUpdated { get; set; }


        // User and Profile data
        public int ProfileId { get; set; }
        public float HoursPlayed { get; set; }
        public List<Platform>? OwnedPlatforms { get; set; } = new List<Platform>();
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? DroppedDate { get; set; }
        public DateTime? LastPlayed { get; set; }

    }

    public enum Status
    {
        Backlog,
        Playing,
        Completed,
        Dropped,
        Wishlisted
    }
}