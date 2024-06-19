using Deli.DATA.DTOs.Item;

namespace Deli.DATA.DTOs
{

    public class ItemOrderDto
    {   
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public ItemDto Item { get; set; } // Ensure this exists
    }
}
