using Microsoft.AspNetCore.Identity;

namespace MVC_MiniProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
