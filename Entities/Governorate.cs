namespace Deli.Entities
{
    public class Governorate : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public List<Inventory>? Inventories { get; set; }
        public int DeliveryPrice { get; set; }
    }
}
