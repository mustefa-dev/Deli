using System.ComponentModel.DataAnnotations.Schema;

namespace Deli.Entities
{
    public class ItemOrder : BaseEntity<Guid>
    {
        public Guid CartId { get; set; }
        [ForeignKey((nameof(CartId)))]
        public Cart? Cart { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

    }
}
