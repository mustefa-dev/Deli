namespace Deli.DATA.DTOs
{

    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        
        public string Name { get; set; }
        public double? ItemPrice { get; set; }
        public string? Image { get; set; }
    }
}
