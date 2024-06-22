using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class OrderForm
    { 
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        //[Required] public List<OrderItemForm> ItemId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? StreetAddress { get; set; }
        [Required]
        public Guid? GovernorateId { get; set; }
        //public string? GovernorateName{ get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

        
        
        
    }
    
}
