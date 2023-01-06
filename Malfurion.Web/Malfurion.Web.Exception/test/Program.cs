using Malfurion.Web.Exception;
using Malfurion.Web.Exception.Verifiers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMalfurionWebException();
var app = builder.Build();
app.UseMalfurionWebException();

app.MapGet("/", () =>
{
    throw new Exception("test");
});
app.MapGet("/1", () =>
{
    string str = "adqw12312__";
    StringVerifier.OnlyLettersAndNumbers(str);
    throw new BadRequest("badreuqest");
});
app.MapGet("/2", () =>
{
    throw new Unauthorized("unauthorized");
});

app.Run();
