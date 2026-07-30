namespace GamersDock.Entities
{
    public class JournalEntry
    {
        public int JournalEntryId { get; set; }
        public int profileId { get; set; }
        public int GameId { get; set; }
        public int LibraryEntryId { get; set; }

        public DateTime CreatedAt { get; set; }
        public int HoursAtEntry { get; set; }
        public string? Note { get; set; }
        public string? Mood { get; set; }
    }

}
