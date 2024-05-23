using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class OrderItemForm
    {
        [Required] public Guid CarId { get; set; }
        [Required] public double RentalDuration { get; set; }
    }
}
