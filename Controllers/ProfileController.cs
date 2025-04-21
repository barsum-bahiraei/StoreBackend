using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Data;

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
        var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
        return Ok(user);
    }
}
