namespace Services;
using Autofac;

public class Installer : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(i => i.Name.EndsWith("Service"))
                .AsSelf().InstancePerLifetimeScope();
    }
}
