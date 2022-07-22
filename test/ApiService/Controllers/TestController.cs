namespace ApiService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Malfurion.WebApi.Constants;
using Malfurion.WebApi.Models;
using Malfurion.WebApi.Services;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly IHttpClientFactory _httpService;
    public TestController(
        ILogger<TestController> logger,
        IHttpClientFactory httpService)
    {
        _logger = logger;
        _httpService = httpService;
    }

    [HttpGet("[action]")]
    public IActionResult GetTest1()
    {
        _logger.LogInformation($"GetTest1: {HttpContext.TraceIdentifier}");
        return Ok($"GetTest1: {HttpContext.TraceIdentifier}");
    }

    [HttpGet("[action]")]
    public IActionResult GetTest2()
    {
        _logger.LogInformation($"GetTest2: {HttpContext.TraceIdentifier}");
        var client = _httpService.CreateClient(HttpClientName.Internal);
        var result = client.GetAsync("http://localhost:5142/api/test/gettest1").Result;
        return Ok(result);
    }

    [HttpGet("[action]")]
    public IActionResult GetTest3()
    {
        _logger.LogInformation($"GetTest3: {HttpContext.TraceIdentifier}");
        var client = _httpService.CreateClient(HttpClientName.Internal);
        var result = client.GetAsync("http://localhost:5142/api/test/gettest2").Result;
        return Ok(result);
    }
}