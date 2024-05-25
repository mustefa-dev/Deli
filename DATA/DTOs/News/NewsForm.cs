using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class NewsForm 
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public List<string> Images {get; set;}
    }
}
