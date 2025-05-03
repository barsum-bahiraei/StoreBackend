using System.ComponentModel.DataAnnotations;

namespace StoreBackend.Models;

public class UserCreateModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 20 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Family is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Family must be between 6 and 20 characters.")]
    public string Family { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
    public string Password { get; set; }
}
