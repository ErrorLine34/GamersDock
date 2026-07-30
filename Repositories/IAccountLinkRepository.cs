using GamersDock.Entities;
using GamersDock.Dtos;

namespace GamersDock.Repositories
{
    public interface IAccountLinkRepository
    {
        Task<AccountLink?> GetAsync(int profileId);
        Task<AccountLink?> UpdateAsync(int profileId, UpdateAccountLinksRequest request);
    }
}
