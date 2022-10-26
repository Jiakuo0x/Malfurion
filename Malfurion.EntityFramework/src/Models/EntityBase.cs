namespace Malfurion.EntityFramework.Models;

public class EntityBase
{
    [Key]
    public int Id { get; set; }

    [Precision(0)]
    public DateTime Created { get; set; }
    [Precision(0)]
    public DateTime LastUpdated { get; set; }
}