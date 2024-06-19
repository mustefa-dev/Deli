namespace Deli.Entities
{
    public class QualityTools : BaseEntity<Guid>
    {
        public string? Title { get; set; } = "";
        public string? ArTitle { get; set; } = "";
        
        public string? Description { get; set; }= "";
        public string? ArDescription { get; set; }= "";
        public string? Image { get; set; }= "";
        public string? RedirectButton { get; set; }= "";
    }
}
