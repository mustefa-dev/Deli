using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class OrderForm
    {
        [Required] public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        [Required] public List<OrderItemForm> ItemId { get; set; }
    }
    
}
