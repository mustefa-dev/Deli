namespace Deli.Entities
{
    public class Item : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public  string MainDetails { get; set; }
        public string? Description { get; set; }
        public string[] imaages { get; set; }
        public double? Price { get; set; }
        public double? SalePrice { get; set; }
        public double? SalePercintage { get; set; }
        public DateTime? SaleStartDate { get; set; }
        public DateTime? SaleEndDate { get; set; }
        public int? Quantity { get; set; }
        public Guid? InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    
        public Guid? SaleId { get; set; }

    }
}
