using Microsoft.AspNetCore.Mvc;
using PrimitiveApi.Models;
using PrimitiveApi.Services;

namespace PrimitiveApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FormController(MainContext context) : ControllerBase
{
    public async Task<IActionResult> AddApplication([FromBody] Application application)
    {
        context.Applications.Add(application);
        await context.SaveChangesAsync();
        return Ok("Application added");
    }
}