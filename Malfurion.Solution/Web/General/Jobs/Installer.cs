using System.Reflection;

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
        Assembly assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(JobBase)));
        foreach (var type in types)
        {
            var job = Activator.CreateInstance(type) as JobBase;
            if(job == null) continue;
            RecurringJob.AddOrUpdate(job.JobName, () => job.Execute(), job.CronExpression);
        }
    }
}