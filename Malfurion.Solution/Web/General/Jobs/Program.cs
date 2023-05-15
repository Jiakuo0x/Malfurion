var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(configuration => configuration
    .UseSerilogLogProvider()
    .UseInMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();
app.UseHangfireDashboard();

Installer.InstallSerilog();
Installer.InstallJobs();

app.Run();