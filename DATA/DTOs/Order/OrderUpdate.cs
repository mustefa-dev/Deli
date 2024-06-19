namespace Deli.DATA.DTOs
{

    public class OrderUpdate
    {
        public string? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? Note { get; set; }
        public string? Address { get; set; }
        public ICollection<OrderItemUpdate> OrderItemUpdate { get; set; }      
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public double? Rating { get; set; }
        public string? DateOfAccepted { get; set; }
        public string? DateOfCanceled { get; set; }
        public string? DateOfDelivered { get; set; }
        public double TotalPrice { get; set; }
        public string orderstatus { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public string? GovernorateId { get; set; }
        public string? GovernorateName{ get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
