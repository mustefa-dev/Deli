namespace Deli.DATA.DTOs
{

    public class ItemFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public  string? MainDetails { get; set; }
        public string? Description { get; set; }
        public string? RefNumber { get; set; }
        public double? StartPrice { get; set; }=0;
        public float? AvgRating { get; set; }
        public bool? IsSale { get; set; }
        public double? EndPrice { get; set; }
        public int? Quantity { get; set; }
        public Guid? InventoryId { get; set; }
        public Guid? CategoryId { get; set; }

    }
}
