using System.ComponentModel.DataAnnotations;

namespace GamersDock.Entities
{
    public class Achievements
    {
        public int GameId { get; set; } // FK
        [Key]
        public string? AchievementId { get; set; } // PK
        public string? AchievementName { get; set; }
        public Platform? AchievementPlatform { get; set; }
        public string? AchievementType { get; set; }
        public string? AchievementDescription { get; set; }
    }
}