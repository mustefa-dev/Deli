namespace Deli.DATA.DTOs
{

    public class FeedBackFilter : BaseFilter 
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public int? PhoneNumber { get; set; }
        public FeddBackType? Type { get; set; }
    }
}
