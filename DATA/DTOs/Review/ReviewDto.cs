namespace Deli.DATA.DTOs
{

    public class ReviewDto : BaseDto<Guid>
    {
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public int? UserImage { get; set; }
        public string? UserName { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? UserId { get; set; }
    }
}
