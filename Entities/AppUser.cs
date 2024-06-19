namespace Deli.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public string? Email { get; set; }
        
        public string? FullName { get; set; }
        
        public string? Password { get; set; }
        
        public UserRole? Role { get; set; }
        public string? OTP { get; set; }
        public bool? OTPrequired { get; set; } = true;
        public bool? IsSubbedToNews { get; set; } = false;
        public string? SubbedEmail { get; set; }

        public bool? IsActive { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public Guid? AddressId { get; set; }
        public Guid? GovernorateId { get; set; }

        public List<Review> Reviews { get; set; }
        

        public string? FacebookId { get; set; }
        public string? FacebookAccessToken { get; set; }

    
    }
    public enum UserRole
    {
       
        Admin = 0,
        User = 1,
    }
    
}