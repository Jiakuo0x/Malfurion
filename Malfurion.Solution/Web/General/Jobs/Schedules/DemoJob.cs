namespace Jobs.Schedules;

public class DemoJob : JobBase
{
    public override string JobName => nameof(DemoJob);
    public override string CronExpression => Cron.Minutely();
    public override async Task Execute()
    {
        await Task.Run(() => 
        {
            Logger.Information("DemoJob executed");
            throw new Exception("DemoJob failed");
        });
    }
}