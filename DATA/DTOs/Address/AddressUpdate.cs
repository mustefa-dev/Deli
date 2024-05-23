using Deli.Entities;

namespace Deli.DATA.DTOs
{

    public class AddressUpdate
    {
        public string? Name { get; set; }
        public string? FullAddress { get; set; }
        public double? Latidute { get; set; }
        public double? Longitude { get; set; }
        public bool? IsMain { get; set; }
        public Guid? GovernorateId { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
