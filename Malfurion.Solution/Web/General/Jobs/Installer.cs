namespace Jobs;

public static class Installer
{
    public static void InstallSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File($"logs\\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
    public static void InstallJobs()
    {
        RecurringJob.AddOrUpdate<DemoJob>(nameof(DemoJob), job => job.Execute(), Cron.Minutely());
        RecurringJob.AddOrUpdate<Demo2Job>(nameof(Demo2Job), job => job.Execute(), Cron.Minutely());
    }
}