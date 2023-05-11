namespace Services.Demo;

public class DemoService
{
    private readonly DbContext _dbContext;
    public DemoService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DemoModel TestAdd(string name)
    {
        var model = new DemoModel
        {
            Name = name
        };
        _dbContext.Set<DemoModel>().Add(model);
        _dbContext.SaveChanges();
        return model;
    }
    public object TestList()
    {
        return _dbContext.Set<DemoModel>().ToList();
    }
}