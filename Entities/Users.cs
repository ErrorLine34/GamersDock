using Microsoft.AspNetCore.Identity;

namespace GamersDock.Entities
{
    public class Users : IdentityUser
    {
        public ICollection<Profiles>? Profiles { get; set; } = new List<Profiles>();
    }
}