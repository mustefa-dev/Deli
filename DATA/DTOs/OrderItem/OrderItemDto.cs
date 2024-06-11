namespace Deli.DATA.DTOs
{

    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string? Image { get; set; }
    }
}
