namespace Deli.DATA.DTOs
{

    public class FeedBackDto : BaseDto<Guid>
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public int? PhoneNumber { get; set; }
        
        public FeddBackType? Type { get; set; }
        public string? Message { get; set; }
    }
}
