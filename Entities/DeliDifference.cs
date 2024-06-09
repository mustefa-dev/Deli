namespace Deli.Entities
{
    public class DeliDifference : BaseEntity<Guid>
    {
        public string? Title { get; set; } = "";
        public string? Description { get; set; }= "";
        public string? Image { get; set; }= "";
    }
}