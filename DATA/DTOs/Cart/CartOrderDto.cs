namespace Deli.DATA.DTOs.cart
{
    public class CartOrderDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        
        public int Quantity { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string? Image { get; set; }
        
    }
}