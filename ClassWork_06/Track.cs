namespace Сlass04
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public int AlbumID { get; set; }
        public Album Album { get; set; }

        
        public int? Rating { get; set; }
        public long ListenCount { get; set; }
        public string? Lyrics { get; set; }
    }
}
