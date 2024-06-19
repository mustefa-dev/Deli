using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class OrderForm
    {
        [Required] public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        [Required] public List<OrderItemForm> ItemId { get; set; }
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public Guid? GovernorateId { get; set; }
        public string? GovernorateName{ get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        
        
        
    }
    
}
