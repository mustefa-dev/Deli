namespace Deli.DATA.DTOs
{

    public class ReviewFilter : BaseFilter 
    {
        public int? Rating { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? UserId { get; set; }
    }
}
