namespace GamersDock.Entities
{
    public class StoreLink
    {
        public int StoreLinkId { get; set; }
        public int GameId { get; set; }
        public string? StoreName { get; set; }
        public string? Url { get; set; }
        public float? CurrentDiscountPercentage { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}