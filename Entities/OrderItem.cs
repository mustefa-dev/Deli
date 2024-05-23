namespace Deli.Entities;

public class OrderItem
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    
    public int Quantity { get; set; }

}