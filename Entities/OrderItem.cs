namespace Deli.Entities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? PackageId { get; set; }
        public Package? Package { get; set; }
      
        public Item? Item { get; set; }
        public bool IsPackage { get; set; }=false;
        public double? ItemPrice { get; set; }
        public int Quantity { get; set; }

    }
}
