using System.ComponentModel.DataAnnotations;

namespace GamersDock.Entities
{
    // Per profile unlock state (ProfileId, AchievementId).
    public class ProfileAchievements
    {
        [Key]
        public int ProfileAchievementId { get; set; } //PK
        public int ProfileId { get; set; } // FK
        public string? AchievementId { get; set; } // FK -> Achievements

        public bool Unlocked { get; set; }
        public DateTime? UnlockedDate { get; set; }
    }
}