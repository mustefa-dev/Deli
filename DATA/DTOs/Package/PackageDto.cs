using Deli.DATA.DTOs.Item;

namespace Deli.DATA.DTOs
{

    public class PackageDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<ItemDto> Items { get; set; }
        public double? DiscountPercentage { get; set; }

        public double? Price { get; set; }
      
    }
}
