var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext, DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("App"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test/list", (DbContext db) => Results.Ok(db.Set<DemoModel>().ToList()));

app.Run();
