namespace Deli.Entities
{
    public class Inventory : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public Guid? GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        public List<Item>? Items { get; set; }

    }
}
