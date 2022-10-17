var result = Malfurion.AutoMapper.Mapper.Map<Target>(new { Id = 100, C = new { Name = "Test" } });
var test = 1;

public class Source
{
    public int Id { get; set; }
    public A? C { get; set; }
    public class A
    {
        public string Name { get; set; } = string.Empty;
    }
}
public class Target
{
    public int Id { get; set; }
    public B? C { get; set; }
    public class B
    {
        public string Name { get; set; } = string.Empty;
    }
}