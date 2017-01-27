using Autofac;
using GroceryCo.KioskSystem.AppDefinitions;
using GroceryCo.KioskSystem.Configuration;

namespace GroceryCo.KioskSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            IContainer container = ContainerConfig.Configure();

            using (var lifetimeScope = container.BeginLifetimeScope())
            {
                var app = lifetimeScope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}
