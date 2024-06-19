using Deli.DATA.DTOs.cart;
using Deli.DATA.DTOs.User;
using Deli.Entities;

namespace Deli.DATA.DTOs.Cart
{

    public class CartDto:BaseDto<Guid>
    {
        public List<CartOrderDto> CartOrderDto { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public string UserNAme { get; set; }
        public UserDto User { get; set; }
        public List<ItemOrderDto> ItemOrders { get; set; } // Add this line
    }
}
