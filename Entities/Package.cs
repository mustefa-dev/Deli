namespace Deli.Entities
{
    public class Package : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public string? Description { get; set; }
        public string? ArDescription { get; set; }
        public string? Image { get; set; }
        
        public List<Item>? Items { get; set; } = new List<Item>(); 
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
