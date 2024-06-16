

namespace Deli.Entities
{
    public class NewsSubscribedUser : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        
        public string? Email { get; set; }
    }
}
