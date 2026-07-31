using GamersDock.Entities;
using GamersDock.Data;
using GamersDock.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GamersDock.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GamersDockContext _context;

        public GameRepository(GamersDockContext context)
        {
            _context = context;
        }

        public async Task<List<Games>> GetLibraryAsync(int profileId)
        {
            var entries = await _context.LibraryEntries
                .Where(le => le.ProfileId == profileId)
                .ToListAsync();

            var gameIds = entries.Select(le => le.GameId).ToList();

            var games = await _context.Games
                .Where(g => gameIds.Contains(g.GameId))
                .Include(g => g.GameMedias)
                .Include(g => g.Genres)
                .Include(g => g.Platforms)
                .ToListAsync();

            foreach (var game in games)
            {
                game.LibraryEntry = entries.FirstOrDefault(le => le.GameId == game.GameId);
            }

            return games;
        }

        public async Task<Games?> GetByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.GameMedias)
                .Include(g => g.Genres)
                .Include(g => g.Platforms)
                .Include(g => g.StoreLinks)
                .Include(g => g.PriceHistory)
                .FirstOrDefaultAsync(g => g.GameId == id);
        }

        public async Task<bool> AddToLibraryAsync (int id, int profileId, int platformId)
        {
            // Check if the game exists in the library for a profile
            var entry = await _context.LibraryEntries
                .Include(le => le.OwnedPlatforms)
                .FirstOrDefaultAsync(le => le.GameId == id && le.ProfileId == profileId);

            // Check if the game already exists in the library for the specified platform
            var entryPlatform = await _context.LibraryEntries
                .FirstOrDefaultAsync(le => le.OwnedPlatforms.Any(p => p.PlatformId == platformId) && le.GameId == id && le.ProfileId == profileId);

            Platform? existingPlatform = await _context.Platforms.FirstOrDefaultAsync(p => p.PlatformId == platformId);


            // If the game already exists in the library for the specified platform, return false
            if (entryPlatform is not null)
            {
                return false;
            }

            // If the does not exist, create a new entry
            if (entry is null)
            {
                entry = new LibraryEntry
                {
                    GameId = id,
                    ProfileId = profileId,
                    Status = Status.Backlog,
                    DateAdded = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    OwnedPlatforms = new List<Platform>()
                };

                entry.OwnedPlatforms = new List<Platform>();

                await _context.LibraryEntries.AddAsync(entry);
            }

            // Add the platform to the library entry if it exists
            if (existingPlatform is not null)
            {
                entry.OwnedPlatforms.Add(existingPlatform);
            }
            else
            {

                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFromLibraryAsync(int id, int profileId, int platformId)
        {
            var entry = await _context.LibraryEntries
                .Include(le => le.OwnedPlatforms)
                .FirstOrDefaultAsync(le => le.GameId == id && le.ProfileId == profileId);

            if (entry is null) return false;
            if (entry.OwnedPlatforms is null || !entry.OwnedPlatforms.Any()) return false;

            var platformToRemove = entry.OwnedPlatforms.FirstOrDefault(p => p.PlatformId == platformId);
            if (platformToRemove is null) return false;

            // Remove the association
            entry.OwnedPlatforms.Remove(platformToRemove);

            // If no platforms remain, remove the whole library entry
            if (entry.OwnedPlatforms.Count == 0)
            {
                _context.LibraryEntries.Remove(entry);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusManualDateAsync(int id, int profileId, UpdateStatusRequest request, DateTime? manualDate)
        {
            var entry = await GetOrCreateLibraryEntryAsync(id, profileId);

            entry.Status = request.Status;
            entry.LastUpdated = DateTime.UtcNow;

            switch (request.Status)
            {
                case Status.Playing when entry.StartedDate is null:
                    entry.StartedDate = manualDate;
                    break;
                case Status.Completed:
                    entry.CompletedDate = manualDate;
                    break;
                case Status.Dropped:
                    entry.DroppedDate = manualDate;
                    break;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, int profileId, UpdateStatusRequest request)
        {
            var entry = await GetOrCreateLibraryEntryAsync(id, profileId);

            entry.Status = request.Status;
            entry.LastUpdated = DateTime.UtcNow;

            switch (request.Status)
            {
                case Status.Playing when entry.StartedDate is null:
                    entry.StartedDate = DateTime.UtcNow;
                    break;
                case Status.Completed:
                    entry.CompletedDate = DateTime.UtcNow;
                    break;
                case Status.Dropped:
                    entry.DroppedDate = DateTime.UtcNow;
                    break;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRatingAsync(int id, int profileId, UpdateRatingRequest request)
        {
            var entry = await _context.LibraryEntries
                .FirstOrDefaultAsync(le => le.GameId == id && le.ProfileId == profileId);

            if (entry is null) return false;

            entry.Rating = request.Rating;
            entry.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleAchievementAsync(int id, string achievementId, int profileId, UpdateAchievementRequest request)
        {

            var achievementKey = achievementId.ToString();

            var achievementExists = await _context.Achievements
                .AnyAsync(a => a.AchievementId == achievementKey && a.GameId == id);
            if (!achievementExists) return false;

            var profileAchievement = await _context.ProfileAchievements
                .FirstOrDefaultAsync(pa => pa.ProfileId == profileId && pa.AchievementId == achievementKey);

            if (profileAchievement is null)
            {
                profileAchievement = new ProfileAchievements
                {
                    ProfileId = profileId,
                    AchievementId = achievementKey,
                    Unlocked = request.Unlocked,
                    UnlockedDate = request.Unlocked ? DateTime.UtcNow : null
                };
                await _context.ProfileAchievements.AddAsync(profileAchievement);
            }
            else
            {
                profileAchievement.Unlocked = request.Unlocked;
                profileAchievement.UnlockedDate = request.Unlocked ? DateTime.UtcNow : null;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JournalEntry> AddJournalEntryAsync(int id, int profileId, CreateJournalEntryRequest request)
        {
            var libraryEntry = await _context.LibraryEntries
                .FirstOrDefaultAsync(le => le.GameId == id && le.ProfileId == profileId);

            var journalEntry = new JournalEntry
            {
                profileId = profileId,
                GameId = id,
                LibraryEntryId = libraryEntry?.LibraryEntryId ?? 0,
                CreatedAt = DateTime.UtcNow,
                HoursAtEntry = (int)(libraryEntry?.HoursPlayed ?? 0f),
                Note = request.Note,
                Mood = request.Mood
            };

            await _context.JournalEntries.AddAsync(journalEntry);
            await _context.SaveChangesAsync();
            return journalEntry;
        }

        public async Task<Games> CreateAsync(CreateGameRequest request)
        {
            var game = new Games
            {
                ExternalId = request.ExternalId,
                Name = request.Name,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                Developer = request.Developer,
                Publisher = request.Publisher,
                BasePrice = request.BasePrice,
                Metascore = request.Metascore,
                FranchiseId = request.FranchiseId,
                EditionLabel = request.EditionLabel,
                AverageRating = 0f
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game is null) return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<LibraryEntry> GetOrCreateLibraryEntryAsync(int gameId, int profileId)
        {
            var entry = await _context.LibraryEntries
                .FirstOrDefaultAsync(le => le.GameId == gameId && le.ProfileId == profileId);

            if (entry is not null) return entry;

            entry = new LibraryEntry
            {
                GameId = gameId,
                ProfileId = profileId,
                Status = Status.Backlog,
                DateAdded = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            await _context.LibraryEntries.AddAsync(entry);
            return entry;
        }
    }
}
