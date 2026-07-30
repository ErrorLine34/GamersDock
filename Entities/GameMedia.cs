namespace GamersDock.Entities
{
    public class GameMedia
    {
        public int GameMediaId { get; set; }
        public int GameId { get; set; }
        public string? Url { get; set; }
        public MediaType Type { get; set; }
    }
    public enum MediaType
    {
        Image,
        Video,
        Gif
    }
}