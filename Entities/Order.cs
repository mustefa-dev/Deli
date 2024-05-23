using Newtonsoft.Json;

namespace Deli.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        
        public AppUser? User { get; set; }
        
        public Guid? ProviderId { get; set; }
        
        public Guid? AddressId { get; set; }

        public Address? Address { get; set; }

        public DateTime? OrderDate { get; set; }
        
        public OrderStatus? OrderStatus { get; set; }

        public decimal? Latitude { get; set; }
        
        public decimal? Longitude { get; set; }
        
        public string? Note { get; set; }
        public DateTime? DateOfAccepted { get; set; }
        public DateTime? DateOfCanceled { get; set; }
        public DateTime? DateOfDelivered { get; set; }
        public double? Rating { get; set; }
        [JsonIgnore]

        public List<OrderItem>? OrderItem { get; set; }
    }
    public enum OrderStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Delivered = 4,
        Canceled = 5,
    }
}
