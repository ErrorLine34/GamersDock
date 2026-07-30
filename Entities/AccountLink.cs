namespace GamersDock.Entities
{
    public class AccountLink
    {
        public int AccountLinkId { get; set; }
        public int ProfileId { get; set; } // FK

        public bool SteamLinked { get; set; }
        public string? SteamAccessToken { get; set; }

        public bool XboxLinked { get; set; }
        public string? XboxAccessToken { get; set; }
        public string? XboxRefreshToken { get; set; }

        // Uses instance service account to search for PsnUsername
        // Not a link
        public string? PsnUsername { get; set; }
    }
}