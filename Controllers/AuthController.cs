using Microsoft.AspNetCore.Mvc;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;
using StoreBackend.Services.Implementation;

namespace StoreBackend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginParameters parameters)
    {
        var isValidUser = await userService.IsValidUser(parameters);
        if (isValidUser)
        {
            var token = userService.GenerateJwtToken(parameters.Email);
            return Ok(ResponseHelper.Success(token));
        }
        return Unauthorized();
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserCreateParameters parameters, CancellationToken cancellation)
    {
        var result = await userService.Create(parameters, cancellation);
        return Ok(ResponseHelper.Success(result));
    }
}
