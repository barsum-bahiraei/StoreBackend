using System.ComponentModel.DataAnnotations;

namespace StoreBackend.Models;

public class LoginDto
{
    [Required(ErrorMessage = "Username is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 20 characters.")]
    public string Password { get; set; }
}
