using Malfurion.WebApi.Constants;
using Malfurion.WebApi.Middlewares;
using Malfurion.WebApi.HttpMessageHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<RequestId>();
builder.Services.AddTransient<RequestChain>();
builder.Services.AddHttpClient(HttpClientName.Internal)
    .AddHttpMessageHandler<RequestId>()
    .AddHttpMessageHandler<RequestChain>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestIdMiddleware>();
app.UseMiddleware<RequestChainMiddleware>();
app.MapControllers();
app.Run();
