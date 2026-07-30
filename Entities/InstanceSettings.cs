namespace GamersDock.Entities
{
    // Only ever one row
    // always read/write
    // InstanceSettingsId == 1.
    public class InstanceSettings
    {
        public int InstanceSettingsId { get; set; }

        public bool PsnServiceAccountLinked { get; set; }
        public string? PsnServiceAccountLabel { get; set; }
        public string? PsnAccessToken { get; set; }
        public string? PsnRefreshToken { get; set; }

        public string DefaultRegion { get; set; } = "US";
    }
}