using System.ComponentModel.DataAnnotations;

namespace StoreBackend.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 20 characters.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 20 characters.")]
    public string Password { get; set; }
}
