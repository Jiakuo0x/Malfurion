namespace Jobs.Schedules;

public class DemoJob : JobBase
{
    protected override string JobName => nameof(DemoJob);

    private readonly Services.Demo.DemoService _demoService;
    public DemoJob(Services.Demo.DemoService demoService)
    {
        _demoService = demoService;
    }
    public override async Task Execute()
    {
        await Task.Run(() =>
        {
            Logger.Information("test add start");
            _demoService.TestAdd(Guid.NewGuid().ToString());
            Logger.Information("test add end");
        });
    }
}