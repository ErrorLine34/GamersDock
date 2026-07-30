namespace GamersDock.Dtos
{
    // Backed by the InstanceSettings entity, not built yet.
    public record InstanceSettingsDto(bool PsnServiceAccountLinked, string? PsnServiceAccountLabel, string DefaultRegion);

    // Npsso is only sent when (re)connecting the PSN service account
    // the actual token exchange happens server side, never return the raw
    // token back out in InstanceSettingsDto.
    public record UpdateInstanceSettingsRequest(string? Npsso, string? DefaultRegion);
}