using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("list")]
    public IResult TestList([FromServices] Services.Demo.DemoService demoService)
    {
        return Results.Ok(demoService.TestList());
    }

    [HttpGet("add")]
    public IResult TestAdd([FromServices] Services.Demo.DemoService demoService, string name)
    {
        return Results.Ok(demoService.TestAdd(name));
    }
}
