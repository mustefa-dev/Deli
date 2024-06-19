using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs
{

    public class NewsForm 
    {
     
        public string? Title { get; set; }
        public string? ArTitle { get; set; }
        public string? Description { get; set; }
         public string? ArDescription { get; set; }
        [Required]
        public string Image { get; set; }
        public List<string> Images {get; set;}
    }
}
