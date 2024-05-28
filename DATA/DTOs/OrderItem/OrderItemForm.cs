using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class OrderItemForm
    {
        [Required] public Guid ItemId { get; set; }
        [Required] public int Quantity { get; set; }
        
    }
}
