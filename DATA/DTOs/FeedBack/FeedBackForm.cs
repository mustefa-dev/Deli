namespace Deli.DATA.DTOs
{

    public class FeedBackForm 
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Message { get; set; }
        
        public FeddBackType? Type { get; set; }

    }
}
