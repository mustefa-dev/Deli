namespace Deli.DATA.DTOs
{

    public class GovernorateDto : BaseDto<Guid>
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public int DeliveryPrice { get; set; }

    }
}
