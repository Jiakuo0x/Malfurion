namespace Jobs.Schedules;
public abstract class JobBase
{
    private static Dictionary<string, Serilog.Core.Logger> _JobLoggersDic = new Dictionary<string, Serilog.Core.Logger>();
    protected abstract string JobName { get; }
    public abstract Task Execute();
    protected readonly Serilog.Core.Logger Logger;

    public JobBase()
    {
        if(_JobLoggersDic.ContainsKey(JobName))
        {
            Logger = _JobLoggersDic[JobName];
            return;
        }

        Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File($"logs\\{JobName}.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        _JobLoggersDic.Add(JobName, Logger);
    }
}