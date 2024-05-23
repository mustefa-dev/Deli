namespace Deli.Entities;

public class Order
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CanceledAt { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public string Note { get; set; }

    public List<OrderItem> Type { get; set; }
    
}
public enum OrderStatus
{
    Pending = 1,
    Accepted = 2,
    Rejected = 3,
    Delivered = 4,
    Canceled = 5,
}