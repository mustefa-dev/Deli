namespace Deli.DATA.DTOs
{

    public class WhoAreWeDto : BaseDto<Guid>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RedirectButton { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
    }
}
