using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Data;
using StoreBackend.Models;

namespace StoreBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;

    public UserController(DatabaseContext context)
    {
        _context = context;
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
            Password = user.Password,
            Username = user.Name,
        });
        _context.SaveChanges();
        return Ok(user);
    }
}
