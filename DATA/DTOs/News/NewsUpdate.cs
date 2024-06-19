using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class NewsUpdate
    {
   
        public string? Title { get; set; }
        public string? ArTitle { get; set; }
        public string? Description { get; set; }
        public string? ArDescription { get; set; }
        [Required]
        public List<string> Images {get; set;}
        [Required]
        public bool isMain { get; set; }
    }
}
