var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddHangfire(configuration => configuration.)

app.Run();
