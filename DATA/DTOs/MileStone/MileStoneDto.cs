namespace Deli.DATA.DTOs
{

    public class MileStoneDto : BaseDto<Guid>
    {
        public int? Year{ get; set; }
        public string? Description { get; set; }
        public string? ArDescription { get; set; }
        public string? Image { get; set; }
    }
}
