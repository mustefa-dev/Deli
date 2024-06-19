using Deli.Entities;

namespace Deli.DATA.DTOs
{

    public class OrderFilter : BaseFilter 
    {
        public string? UserId { get; set; }
        public string? CarId { get; set; }
        public string? Status { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public string? Note { get; set; }
        public double? Rating { get; set; }
        public DateTime? DateOfAccepted { get; set; }
        public DateTime? DateOfCanceled { get; set; }
        public DateTime? DateOfDelivered { get; set; }
        public Guid? AddressId { get; set; }
        public string? OrderNumber { get; set; }
    }
}
