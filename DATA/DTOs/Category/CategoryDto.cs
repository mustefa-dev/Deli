namespace Deli.DATA.DTOs;

public class CategoryDto : BaseDto<Guid>
{ 
    public string? Name { get; set; }
   
    public string? Description { get; set; }
    public int? NumOfItems { get; set; }
    public string? Image { get; set; }
}