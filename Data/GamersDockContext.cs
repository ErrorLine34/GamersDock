using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GamersDock.Entities;

namespace GamersDock.Data
{
    public class GamersDockContext : IdentityDbContext<Users>
    {
        public GamersDockContext(DbContextOptions<GamersDockContext> options) : base(options)
        {
        }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<GameMedia> GameMedias { get; set; }
        public DbSet<StoreLink> StoreLinks { get; set; }
        public DbSet<LibraryEntry> LibraryEntries { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<PlatformSkus> PlatformSkus { get; set; }
        public DbSet<AccountLink> AccountLinks { get; set; }
        public DbSet<InstanceSettings> InstanceSettings { get; set; }
        public DbSet<ProfileAchievements> ProfileAchievements { get; set; }

    }
}
