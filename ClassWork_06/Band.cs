namespace Сlass04
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }
        public Country Country { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
