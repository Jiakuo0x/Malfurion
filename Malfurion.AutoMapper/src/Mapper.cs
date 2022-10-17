namespace Malfurion.AutoMapper;
public static class Mapper
{
    public static RTarget Map<RTarget>(object source)
        where RTarget : new()
    {
        RTarget result = new();

        if (source is null)
            return result;

        Map(source, result);

        return result;
    }

    public static void Map(object source, object target)
    {
        if (source is null)
        {
            target = null!;
            return;
        }
        if (target is null)
            target = new();

        var sourceProperties = source.GetType().GetProperties();
        foreach (var sourceProperty in sourceProperties)
        {
            var targetProperty = target.GetType().GetProperty(sourceProperty.Name);
            if (targetProperty is null)
            {
                continue;
            }
            if (sourceProperty.PropertyType.IsValueType)
            {
                var sourceValue = sourceProperty.GetValue(source);
                targetProperty.SetValue(target, sourceValue);
            }
            else if (sourceProperty.PropertyType.IsClass)
            {
                var sourceValue = sourceProperty.GetValue(source);
                var targetValue = targetProperty.GetValue(target);
                if(targetValue is null)
                {
                    targetValue = Activator.CreateInstance(targetProperty.PropertyType);
                }
                Map(sourceValue!, targetValue!);
            }
        }
    }
}
