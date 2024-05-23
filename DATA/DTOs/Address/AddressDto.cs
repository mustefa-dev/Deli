namespace Deli.DATA.DTOs
{

    public class AddressDto
    {
        public string? Name { get; set; }
        public string? FullAddress { get; set; }
        public double? Latidute { get; set; }
        public double? Longitude { get; set; }
        public bool? IsMain { get; set; }
        public Guid? GovernorateId { get; set; }
        public string? GovernorateName { get; set; }
    }
}
