namespace Malfurion.EntityFramework.Models;

public class GuidEntityBase : EntityBase
{
    [Key]
    public new Guid Id { get; set; }
}