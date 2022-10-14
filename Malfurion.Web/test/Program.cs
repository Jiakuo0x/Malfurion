using Malfurion.Web.Extensions;
using Malfurion.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMalfurionWeb();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<Malfurion.Web.Middlewares.ApiRequestLogMiddleware>();
app.UseMalfurionWeb();

app.MapGet("/test-ok", (Response response) => response.Ok());
app.MapGet("/test-ok2", (Response response) => response.Ok(new { TestResult = "Test" }));
app.MapGet("/test-bad-request", (Response response) => response.BadRequest("Test bad request."));
app.MapGet("/test-exception", () => { throw new Exception("exception test"); });
app.MapGet("/", () => "Hello World!");

app.Run();
