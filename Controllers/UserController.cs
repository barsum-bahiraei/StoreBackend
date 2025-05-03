using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Data;
using StoreBackend.Helpers;
using StoreBackend.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StoreBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(DatabaseContext context, IHashHelper hashHelper) : ControllerBase
{
    [HttpGet("Detail")]
    [Authorize]
    public IActionResult Detail()
    {
        // فرق اتنتیکیتد و اتورایزد
        var userName = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var user = context.Users.FirstOrDefault(u => u.Username == userName);
        return Ok(user);
    }

    [HttpPost("Create")]
    public IActionResult Create(UserCreateModel user)
    {
        var hasUser = context.Users.Any(u => u.Email == user.Email);
        if (hasUser)
        {
            return BadRequest("Email is already in use.");
        }
        context.Users.Add(new Entities.User
        {
            Name = user.Name,
            Family = user.Family,
            Email = user.Email,
            Password = hashHelper.HashSHA256(user.Password),
            Username = user.Email,
        });
        context.SaveChanges();
        return Ok(user);
    }
}
