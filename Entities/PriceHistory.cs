namespace GamersDock.Entities
{
    public class PriceHistory
    {
        public int PriceHistoryId { get; set; }
        public int GameId { get; set; }
        public DateTime RecordedAt { get; set; }
        public string? Region { get; set; }
        public int? StoreLinkId { get; set; }
        public float? DiscountPercent { get; set; }

    }
}
