using GamersDock.Entities;
using GamersDock.Dtos;

namespace GamersDock.Repositories
{
    public interface IGameRepository
    {
        Task<List<Games>> GetLibraryAsync(int profileId);
        Task<Games?> GetByIdAsync(int id);
        Task<Games> CreateAsync(CreateGameRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddToLibraryAsync(int id, int profileId, int platformId);
        Task<bool> DeleteFromLibraryAsync(int id, int profileId, int platformId);
        Task<bool> UpdateStatusAsync(int id, int profileId, UpdateStatusRequest request);
        Task<bool> UpdateStatusManualDateAsync(int id, int profileId, UpdateStatusRequest request, DateTime? manualDate);
        Task<bool> UpdateRatingAsync(int id, int profileId, UpdateRatingRequest request);
        Task<bool> ToggleAchievementAsync(int id, string achievementId, int profileId, UpdateAchievementRequest request);
        Task<JournalEntry> AddJournalEntryAsync(int id, int profileId, CreateJournalEntryRequest request);
    }
}
