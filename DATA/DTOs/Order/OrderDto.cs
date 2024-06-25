using Deli.DATA.DTOs.User;
using Deli.Entities;

namespace Deli.DATA.DTOs
{

    public class OrderDto : BaseDto <Guid>
    {
        public string? OrderDate { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public string? Note { get; set; }
        public AddressDto? Address { get; set; }
        public UserDto? User { get; set; }
        public ICollection<OrderItemDto> OrderItemDto { get; set; }      
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public double? Rating { get; set; }
        public DateTime? DateOfAccepted { get; set; }
        public DateTime? DateOfCanceled { get; set; }
        public DateTime? DateOfDelivered { get; set; }
        public decimal TotalPrice { get; set; }
            public string orderstatus { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public Guid? GovernorateId { get; set; }
        public string? GovernorateName{ get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public long OrderNumber { get; set; }

        

    }
}
