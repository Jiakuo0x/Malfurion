var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<KongConf>(builder.Configuration.GetSection("Kong"));
builder.Services.AddMalfurionWebKong();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
app.MapGet("/register", (RegisterManager register) => register.Register());

app.Run();