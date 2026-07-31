using GamersDock.Entities;
using GamersDock.Data;
using GamersDock.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GamersDock.Repositories
{
    public class InstanceSettingsRepository : IInstanceSettingsRepository
    {
        private const int SingletonId = 1;
        private readonly GamersDockContext _context;

        public InstanceSettingsRepository(GamersDockContext context)
        {
            _context = context;
        }

        public async Task<InstanceSettings> GetAsync()
        {
            var settings = await _context.InstanceSettings.FirstOrDefaultAsync(s => s.InstanceSettingsId == SingletonId);
            if (settings is not null) return settings;

            settings = new InstanceSettings { InstanceSettingsId = SingletonId };
            await _context.InstanceSettings.AddAsync(settings);
            await _context.SaveChangesAsync();
            return settings;
        }

        public async Task<InstanceSettings> UpdateAsync(UpdateInstanceSettingsRequest request)
        {
            var settings = await GetAsync();

            if (request.DefaultRegion is not null)
            {
                settings.DefaultRegion = request.DefaultRegion;
            }

            if (!string.IsNullOrWhiteSpace(request.Npsso))
            {
                // TODO: exchange the Npsso for real PSN access/refresh tokens server-side.
                // For now this just flips the "linked" flag so the flow is wireable end to end;
                // the actual PSN OAuth exchange needs to be implemented before this is real.
                settings.PsnServiceAccountLinked = true;
                settings.PsnServiceAccountLabel ??= "PSN Account";
            }

            await _context.SaveChangesAsync();
            return settings;
        }
    }
}
