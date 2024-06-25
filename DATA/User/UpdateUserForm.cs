namespace Deli.DATA.DTOs.User
{
    public class UpdateUserForm
    {
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? FullName { get; set; }
        

        
        public bool? IsActive { get; set; }
        
    }
}