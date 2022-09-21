namespace Malfurion.EntityFramework.Models;

public class EntityBase
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdateOn { get; set; }
}