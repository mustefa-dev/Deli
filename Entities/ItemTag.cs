namespace Deli.Entities
{
    public class ItemTag : BaseEntity<Guid>
    {
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
