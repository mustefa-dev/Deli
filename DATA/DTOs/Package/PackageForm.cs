namespace Deli.DATA.DTOs
{

    public class PackageForm 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> ItemIds { get; set; } = new List<Guid>(); 
        
        public decimal DiscountPercentage { get; set; }
    }
}
