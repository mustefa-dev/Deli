using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class OrderForm
    {
        [Required] public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        [Required] public Guid ItemId { get; set; }
        [Required] public double RentalDuration { get; set; }
    }
    
}
