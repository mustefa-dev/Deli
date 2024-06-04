using Deli.Entities;

namespace Deli.DATA.DTOs;

public class OrderStatisticsDto
{
    public int TotalOrders { get; set; }
    public int AcceptedOrders { get; set; }
    public int RejectedOrders { get; set; }
    public int PendingOrders { get; set; }
}

public class OrderStatisticsFilter
{
    public Guid? InventoryId { get; set; }
    public Guid? CategoryId { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}