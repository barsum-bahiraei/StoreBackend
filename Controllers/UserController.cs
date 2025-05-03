using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Data;
using StoreBackend.Helpers;
using StoreBackend.Models;

namespace StoreBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly HashHelper _hashHelper;

    public UserController(DatabaseContext context, HashHelper hashHelper)
    {
        _context = context;
        _hashHelper = hashHelper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        // فرق اتنتیکیتد و اتورایزد
        //var user = _context.Users.Where(x=>x.Id == id).FirstOrDefault();
        return Ok("user");
    }

    [HttpPost]
    public IActionResult Create(UserCreateModel user)
    {
        _context.Users.Add(new Entities.User
        {
            Name = user.Name,
            Family = user.Family,
            Email = user.Email,
            Password = _hashHelper.HashSHA256(user.Password),
            Username = user.Name,
        });
        _context.SaveChanges();
        return Ok(user);
    }
}
