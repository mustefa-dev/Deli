using Deli.DATA.DTOs.Item;

namespace Deli.DATA.DTOs
{

    public class PackageDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemDto> Items { get; set; }
        public decimal DiscountPercentage { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = Items.Sum(item => (decimal)(item.Price ?? 0.0));
                decimal discountAmount = totalPrice * DiscountPercentage / 100;
                return totalPrice - discountAmount;
            }
        }
    }
}
