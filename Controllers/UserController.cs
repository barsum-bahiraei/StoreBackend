using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Data;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace StoreBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(DatabaseContext context, IConfiguration configuration, IUserService userService) : ControllerBase
{
    [HttpGet("Detail")]
    [Authorize]
    public IActionResult Detail()
    {
        // فرق اتنتیکیتد و اتورایزد
        var email = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

        var user = context.Users.FirstOrDefault(u => u.Email == email);
        return Ok(user);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserCreateDTO user)
    {
        var newUser = await userService.Create(user);
        return Ok(newUser);
    }
}
