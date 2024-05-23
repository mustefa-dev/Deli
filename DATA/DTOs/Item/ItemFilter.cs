namespace Deli.DATA.DTOs
{

    public class ItemFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public  string? MainDetails { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public Guid? InventoryId { get; set; }
        public Guid? CategoryId { get; set; }

    }
}
