using GamersDock.Entities;
using GamersDock.Data;
using GamersDock.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GamersDock.Repositories
{
    public class AccountLinkRepository : IAccountLinkRepository
    {
        private readonly GamersDockContext _context;

        public AccountLinkRepository(GamersDockContext context)
        {
            _context = context;
        }

        public async Task<AccountLink?> GetAsync(int profileId)
        {
            var profileExists = await _context.Profiles.AnyAsync(p => p.ProfileId == profileId);
            if (!profileExists) return null;

            return await GetOrCreateAsync(profileId);
        }

        public async Task<AccountLink?> UpdateAsync(int profileId, UpdateAccountLinksRequest request)
        {
            var profileExists = await _context.Profiles.AnyAsync(p => p.ProfileId == profileId);
            if (!profileExists) return null;

            var link = await GetOrCreateAsync(profileId);

            if (request.SteamLinked is not null) link.SteamLinked = request.SteamLinked.Value;
            if (request.XboxLinked is not null) link.XboxLinked = request.XboxLinked.Value;
            if (request.PsnUsername is not null) link.PsnUsername = request.PsnUsername;

            await _context.SaveChangesAsync();
            return link;
        }

        private async Task<AccountLink> GetOrCreateAsync(int profileId)
        {
            var link = await _context.AccountLinks.FirstOrDefaultAsync(a => a.ProfileId == profileId);
            if (link is not null) return link;

            link = new AccountLink { ProfileId = profileId };
            await _context.AccountLinks.AddAsync(link);
            await _context.SaveChangesAsync();
            return link;
        }
    }
}
