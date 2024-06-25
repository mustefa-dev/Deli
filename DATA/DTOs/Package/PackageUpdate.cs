namespace Deli.DATA.DTOs
{

    public class PackageUpdate
    {
        public string? Name { get; set; }
        public string? ArName { get; set; } 
        public string? Description { get; set; }
        public string? ArDescription { get; set; }
        public string? Image { get; set; }
        public List<Guid>? ItemIds { get; set; }
        public double? Price { get; set; }
    }
}
