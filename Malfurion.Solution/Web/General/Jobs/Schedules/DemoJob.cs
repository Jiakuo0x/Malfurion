namespace Jobs.Schedules;

public class DemoJob : JobBase
{
    protected override string JobName => nameof(DemoJob);
    public override async Task Execute()
    {
        await Task.Run(() => 
        {
            Logger.Information("DemoJob executed");
            throw new Exception("DemoJob failed");
        });
    }
}