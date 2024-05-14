namespace Deli.Entities;

public class Message : BaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string FromUser { get; set; }
    public string Messages { get; set; }
    public DateTime Timestamp { get; set; }
}