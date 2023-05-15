var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule<Services.Installer>();
});

builder.Services.AddDbContext<DbContext, DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("App"));
});

builder.Services.AddHangfire(configuration => configuration
    .UseSerilogLogProvider()
    .UseInMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();
app.UseHangfireDashboard();

Jobs.Installer.InstallSerilog();
Jobs.Installer.InstallJobs();

app.Run();