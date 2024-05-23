namespace Deli.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public List<Item>? Items { get; set; }
    }
}
