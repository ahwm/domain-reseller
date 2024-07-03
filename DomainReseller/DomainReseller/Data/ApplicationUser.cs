using Microsoft.AspNetCore.Identity;

namespace DomainReseller.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? GoDaddyCustomerId { get; set; }

        public string? GoDaddyShopperId { get; set; }
    }

}
