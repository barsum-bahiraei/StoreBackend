using Microsoft.AspNetCore.Mvc;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreBackend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    // GET: api/<ValuesController>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await productService.Detail(id);
        return Ok(ResponseHelper.Success(result));
    }

    // POST api/<ValuesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductCreateParameters productCreateDto)
    {
        var productId = await productService.Create(productCreateDto);
        return Ok(ResponseHelper.Success(productId));
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
