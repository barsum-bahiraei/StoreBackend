using Microsoft.AspNetCore.Mvc;
using StoreBackend.Data;
using StoreBackend.Models;

namespace StoreBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly DatabaseContext _context;

    public ProfileController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        // فرق اتنتیکیتد و اتورایزد
        var user = _context.Users.Where(x=>x.Id == id).FirstOrDefault();
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(UserCreate user)
    {
        _context.Users.Add(new Entities.User
        {
            Name = user.Name
        });
        _context.SaveChanges();
        return Ok(user);
    }
}
