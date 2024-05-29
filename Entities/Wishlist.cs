namespace Deli.Entities
{
    public class Wishlist : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public List<Guid>? ItemsIds { get; set; }= new List<Guid>();
    }
}
