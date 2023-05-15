namespace Jobs.Schedules;

public class Demo2Job : JobBase
{
    protected override string JobName => nameof(Demo2Job);
    public override async Task Execute()
    {
        await Task.Run(() => 
        {
            Logger.Information("DemoJob executed");
            throw new Exception("DemoJob failed");
        });
    }
}