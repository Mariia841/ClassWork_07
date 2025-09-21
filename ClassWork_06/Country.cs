namespace Сlass04
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Band> Bands { get; set; } = new List<Band>();
    }
}
