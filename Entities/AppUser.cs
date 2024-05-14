namespace Deli.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public string? Email { get; set; }
        
        public string? FullName { get; set; }
        
        public string? Password { get; set; }
        
        public UserRole? Role { get; set; }

        public bool? IsActive { get; set; }

               
        
    
    }
    public enum UserRole
    {
       
        Admin = 0,
        User = 1,
    }
    
}