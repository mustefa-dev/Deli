namespace Deli.Entities
{
    public class Tag : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ItemTag> ItemTags { get; set; } = new List<ItemTag>();    }
}
