using Malfurion.Web.Exception;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMalfurionWebException();
var app = builder.Build();
app.UseMalfurionWebException();

app.MapGet("/", () =>
{
    throw new Exception("test");
});

app.Run();
