namespace Deli.DATA.DTOs
{

    public class DeliDifferenceDto : BaseDto<Guid>
    {
        public string? Title { get; set; }
        
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
