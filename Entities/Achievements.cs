namespace GamersDock.Entities
{
    public class Achievements
    {
        public int GameId { get; set; } // FK
        public string? AchievementId { get; set; } // PK
        public string? AchievementName { get; set; }
        public Platform? AchievementPlatform { get; set; }
        public string? AchievementType { get; set;}
        public string? AchievementDescription { get; set; }
        public bool AchievementUnlocked { get; set; }
        public DateTime AchivementUnlockDate { get; set; }

    }
}
