namespace Deli.DATA.DTOs
{

    public class PackageUpdate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> ItemIds { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
