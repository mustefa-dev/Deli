using System.ComponentModel.DataAnnotations;
using Deli.Entities;

namespace Deli.Entities
{
    public class News : BaseEntity<Guid>
    {
     
        public string Title { get; set; }
        public string ArTitle { get; set; }
        
        public string Description { get; set; }
        public string ArDescription { get; set; }
   
        public string Image { get; set; }
        public List<string> Images {get; set;}
        [Required]
        public bool isMain { get; set; }
    }
}
