using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Account
{
    public class RegisterVM
    {
        [Required, MaxLength(60)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(40)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>Member | Admin | SuperAdmin</summary>
        public string Role { get; set; } = "Member";
    }
}
