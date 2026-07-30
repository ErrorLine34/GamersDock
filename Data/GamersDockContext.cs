using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GamersDock.Entities;

namespace GamersDock.Data
{
    public class GamersDockContext(DbContextOptions<GamersDockContext> options) : IdentityDbContext<Users>(options)
    {

        public DbSet<Users> Users { get; set; }
        public DbSet<Profiles> Profiles { get; set; }

    }
}
