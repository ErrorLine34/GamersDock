namespace GamersDock.Entities
{
    public class Profiles
    {
        public Profiles() { }

        public int ProfileId { get; set; }
        public string ProfileName { get; set; } = string.Empty;
        public Users? User { get; set; } // Back reference to Users
        public string UserId { get; set; } = string.Empty; // FK
        public int RoleId { get; set; }

    }

    public enum UserRoles
    {
        Admin = 1,
        User = 2
    }
}
