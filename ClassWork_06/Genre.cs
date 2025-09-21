namespace Сlass04
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
