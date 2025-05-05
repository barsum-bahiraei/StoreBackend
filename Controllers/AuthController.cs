using Microsoft.AspNetCore.Mvc;
using StoreBackend.Models;
using StoreBackend.Services;

namespace StoreBackend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("SignIn")]
    public IActionResult Login(LoginModel loginModel)
    {
        if (_authService.IsValidUser(loginModel))
        {
            var token = _authService.GenerateJwtToken(loginModel.UserName);
            return Ok(token);
        }
        return Unauthorized();

    }
}
