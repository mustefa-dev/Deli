namespace Deli.DATA.DTOs.Item
{

    public class ItemDto : BaseDto<Guid>
    {
        public string? Name { get; set; }
        public  string MainDetails { get; set; }
        public string? Description { get; set; }
        public string[] imaages { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public Guid? InventoryId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? InventoryName { get; set; }
        public string? GovernorateName { get; set; }
    }
}
