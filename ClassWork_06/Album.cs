namespace Сlass04
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int BandID { get; set; }
        public Band Band { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        public int? Rating { get; set; }
    }
}
