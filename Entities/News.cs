using System.ComponentModel.DataAnnotations;
using Deli.Entities;

namespace Deli.Entities
{
    public class News : BaseEntity<Guid>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public List<string> Images {get; set;}
        [Required]
        public bool isMain { get; set; }
    }
}
