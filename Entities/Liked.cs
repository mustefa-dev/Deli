namespace Deli.Entities
{
    public class Liked : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public List<Guid>? ItemsIds { get; set; }= new List<Guid>();
    }
}
