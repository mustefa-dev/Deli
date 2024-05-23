using Deli.Entities;

namespace Deli.DATA.DTOs
{

    public class OrderDto : BaseDto <Guid>
    {
        public string? OrderDate { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public string? Note { get; set; }
        public AddressDto? Address { get; set; }
        public ICollection<OrderItemDto> OrderItemDto { get; set; }      
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public double? Rating { get; set; }
        public DateTime? DateOfAccepted { get; set; }
        public DateTime? DateOfCanceled { get; set; }
        public DateTime? DateOfDelivered { get; set; }
        public double TotalPrice { get; set; }
        public string orderstatus { get; set; }

    }
}
