using System.ComponentModel.DataAnnotations;

namespace GamersDock.Entities
{
    public class PlatformSkus
    {
        [Key]
        public int PlatformSkuId { get; set; } // PK
        public int GameId { get; set; } // FK
        public int PlatformId { get; set; } // FK -> Platform
        public string? StoreName { get; set; }
        public string? ExternalId { get; set; } // Steam appid, PS product id, Xbox product id, eShop id, etc.
        public string? StoreUrl { get; set; }
    }
}