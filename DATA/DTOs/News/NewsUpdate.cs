using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class NewsUpdate
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public List<string> Images {get; set;}
        [Required]
        public bool isMain { get; set; }
    }
}
