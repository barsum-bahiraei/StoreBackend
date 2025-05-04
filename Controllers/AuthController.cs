using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreBackend.Data;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
