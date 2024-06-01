namespace Deli.DATA.DTOs
{

    public class ReviewForm 
    {
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public Guid ItemId { get; set; }
    }
}
