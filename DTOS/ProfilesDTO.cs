namespace GamersDock.Dtos
{
    public record ProfileDto(int Id, string Name, string Avatar, bool IsAdmin, DateTime CreatedAt);

    public record CreateProfileRequest(string Name, string Avatar);

    public record UpdateProfileRequest(string Name);

    public record UpdateAvatarRequest(string Avatar);

    // Backed by the AccountLink entity, not built yet.
    public record AccountLinksDto(bool SteamLinked, bool XboxLinked, string? PsnUsername);

    // All fields optional
    // only send what changed.
    public record UpdateAccountLinksRequest(bool? SteamLinked, bool? XboxLinked, string? PsnUsername);
}