namespace Deli.Entities
{
    public class Review : BaseEntity<Guid>
    {
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
