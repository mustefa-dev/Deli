namespace Deli.Entities
{
    public class Sale : BaseEntity<Guid>
    {
        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }
        public double? SalePrice { get; set; }
        public double SalePercintage { get; set; }  // this is automatically calculated from saleprice
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
    }
}