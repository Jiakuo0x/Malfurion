using Malfurion.Web.Responser;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMalfurionWebResponser();
var app = builder.Build();

app.MapGet("/ok", (Response response) => response.Ok("okok"));
app.MapGet("/bad", (Response response) => response.BadRequest("badbad"));

app.Run();
