namespace Deli.Entities
{
    public class Appsettings : BaseEntity<Guid>
    {
        public string? Twitterlink { get; set; } = "";
        public string? Instalink { get; set; } = "";
        public string? Facebooklink { get; set; }= "";
        public string? Linkedinlink { get; set; }= "";
        public string DeliPhoneNumber { get; set; }= "";
        public string DeliEmail { get; set; }= "";
    }
}