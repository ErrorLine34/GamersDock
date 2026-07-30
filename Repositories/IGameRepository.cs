using GamersDock.Entities;
using GamersDock.Dtos;

namespace GamersDock.Repositories
{
    public interface IGameRepository
    {
        Task<List<Games>> GetLibraryAsync(int profileId);
        Task<Games?> GetByIdAsync(int id);
        Task<bool> UpdateStatusAsync(int id, int profileId, UpdateStatusRequest request);
        Task<bool> UpdateRatingAsync(int id, int profileId, UpdateRatingRequest request);
        Task<bool> ToggleAchievementAsync(int id, int achievementId, int profileId, UpdateAchievementRequest request);
        Task<JournalEntry> AddJournalEntryAsync(int id, int profileId, CreateJournalEntryRequest request);
    }
}
