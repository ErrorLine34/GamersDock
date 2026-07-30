using System.ComponentModel.DataAnnotations;

namespace GamersDock.Entities
{
    public class Games
    {
        [Key]
        public int GameId { get; set; }
        public int ExternalId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public float? BasePrice { get; set; }
        public ICollection<PriceHistory>? PriceHistory { get; set; }
        public ICollection<GameMedia>? GameMedias { get; set; }
        public ICollection<StoreLink>? StoreLinks { get; set; }
        public ICollection<Platform>? Platforms { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public float? Metascore { get; set; }
        public float AverageRating { get; set; }
        public string? FranchiseId { get; set; }
        public string? EditionLabel { get; set; }

    }
}