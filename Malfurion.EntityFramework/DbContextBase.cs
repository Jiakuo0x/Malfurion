namespace Malfurion.EntityFramework;

public class DbContextBase : DbContext
{
    public override int SaveChanges()
    {
        var entries = this.ChangeTracker.Entries()
            .Where(i => i.State is not EntityState.Unchanged);

        var utcTime = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            if (entry.Entity is not Models.EntityBase model)
                continue;

            if (entry.State is EntityState.Added)
                model.CreatedOn = utcTime;

            model.LastUpdateOn = utcTime;
        }

        return base.SaveChanges();
    }
}