using GamersDock.Entities;

namespace GamersDock.Dtos
{
    // For list/card views lighter than GameDetailDto.
    public record GameSummaryDto(
        int Id,
        string Name,
        string? CoverUrl,
        float? Metascore,
        Status Status,
        float? Rating,
        float HoursPlayed
    );

    // For the full detail page everything it needs in one payload.
    public record GameDetailDto(
        int Id,
        string Name,
        string? Description,
        DateTime ReleaseDate,
        string? Developer,
        string? Publisher,
        decimal BasePrice,
        float? Metascore,
        float AverageRating,
        IReadOnlyList<string> Genres,
        IReadOnlyList<string> Platforms,
        Status Status,
        float? Rating,
        float HoursPlayed,
        IReadOnlyList<AchievementDto> Achievements,
        IReadOnlyList<StorePriceDto> Stores,
        IReadOnlyList<PricePointDto> PriceHistory,
        IReadOnlyList<JournalEntryDto> Journal,
        string? FranchiseId,
        string? EditionLabel,
        IReadOnlyList<SiblingEditionDto> SiblingEditions,
        IReadOnlyList<PlatformSkuDto> Skus,
        ThirdPartyDealDto? ThirdPartyDeal
    );

    public record AchievementDto(string Id, string? Name, string? Description, bool Unlocked, DateTime? UnlockedDate);

    public record StorePriceDto(string? StoreName, string? Url, decimal? Price, float? DiscountPercent);

    public record PricePointDto(DateTime RecordedAt, string? Region, decimal? Price, float? DiscountPercent);

    public record SiblingEditionDto(int Id, string Name, string EditionLabel);

    // Backed by the PlatformSku entity, not built yet.
    public record PlatformSkuDto(string Platform, string Store, string ExternalId, string StoreUrl);

    public record ThirdPartyDealDto(decimal Price, string Currency, string Url, string Source);

    public record JournalEntryDto(int Id, DateTime CreatedAt, int HoursAtEntry, string? Note, string? Mood);

    // HoursAtEntry is filled server side from the LibraryEntry at the time
    // of creation, not sent by the client.
    public record CreateJournalEntryRequest(string? Note, string? Mood);

    public record UpdateStatusRequest(Status Status);

    public record UpdateRatingRequest(float? Rating);

    public record UpdateAchievementRequest(bool Unlocked);
}