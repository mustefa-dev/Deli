namespace Deli.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public string? Description { get; set; }
        public string? ArDescription { get; set; }
        
        public string? Image { get; set; }
        
        public List<Item>? Items { get; set; }
        public int? NumOfItems { get; set; }
    }
}
