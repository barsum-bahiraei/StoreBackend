using Microsoft.AspNetCore.Mvc;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Implementation;

namespace StoreBackend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("SignIn")]
    public async Task<IActionResult> Login(LoginDTO loginModel)
    {
        var isValidUser = await authService.IsValidUser(loginModel);
        if (isValidUser)
        {
            var token = authService.GenerateJwtToken(loginModel.Email);
            return Ok(ResponseHelper.Success(token));
        }
        return Unauthorized();
    }
}
