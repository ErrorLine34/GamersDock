using GamersDock.Dtos;
using GamersDock.Entities;
using System.Linq;

namespace GamersDock.Extensions
{
    public static class DtoMappingExtensions
    {
        public static UserDto ToDto(this Users u) => new(u.Id, u.UserName);

        public static ProfileDto ToDto(this Profiles p) => new(
            p.ProfileId,
            p.ProfileName,
            p.Avatar ?? string.Empty,
            p.RoleId == (int)UserRoles.Admin,
            p.CreatedAt ?? DateTime.UtcNow
        );

        public static AccountLinksDto ToDto(this AccountLink a) => new(a.SteamLinked, a.XboxLinked, a.PsnUsername);

        public static InstanceSettingsDto ToDto(this InstanceSettings s) => new(s.PsnServiceAccountLinked, s.PsnServiceAccountLabel, s.DefaultRegion);

        public static GameSummaryDto ToSummaryDto(this Games g) => new(
            g.GameId,
            g.Name ?? string.Empty,
            g.GameMedias?.FirstOrDefault()?.Url,
            g.Metascore,
            Status.Backlog,
            null,
            0f
        );

        public static GameDetailDto ToDetailDto(this Games g) => new(
            g.GameId,
            g.Name ?? string.Empty,
            g.Description ?? string.Empty,
            g.ReleaseDate,
            g.Developer,
            g.Publisher,
            (decimal)(g.BasePrice ?? 0f),
            g.Metascore,
            g.AverageRating,
            g.Genres?.Select(x => x.Name ?? string.Empty).ToList() ?? new List<string>(),
            g.Platforms?.Select(x => x.Name ?? string.Empty).ToList() ?? new List<string>(),
            Status.Backlog,
            null,
            0f,
            new List<AchievementDto>(),
            new List<StorePriceDto>(),
            new List<PricePointDto>(),
            new List<JournalEntryDto>(),
            g.FranchiseId,
            g.EditionLabel,
            new List<SiblingEditionDto>(),
            new List<PlatformSkuDto>(),
            null
        );

        public static JournalEntryDto ToDto(this JournalEntry j) => new(j.JournalEntryId, j.CreatedAt, j.HoursAtEntry, j.Note, j.Mood);
    }
}
