namespace Deli.Entities
{
    public class Cart : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<ItemOrder>? ItemOrders { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
