namespace Сlass04
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
