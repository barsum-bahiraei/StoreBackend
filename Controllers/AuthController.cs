using Microsoft.AspNetCore.Mvc;
using StoreBackend.Models;
using StoreBackend.Services.Implementation;

namespace StoreBackend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("SignIn")]
    public IActionResult Login(LoginModel loginModel)
    {
        if (authService.IsValidUser(loginModel))
        {
            var token = authService.GenerateJwtToken(loginModel.UserName);
            return Ok(token);
        }
        return Unauthorized();

    }
}
