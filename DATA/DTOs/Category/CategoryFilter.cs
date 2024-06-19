namespace Deli.DATA.DTOs
{

    public class CategoryFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
