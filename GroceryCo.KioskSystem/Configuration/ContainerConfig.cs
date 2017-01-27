using Autofac;
using GroceryCo.KioskSystem.App;
using GroceryCo.KioskSystem.AppDefinitions;
using GroceryCo.KioskSystem.Core;
using GroceryCo.KioskSystem.Core.KioskDefinitions;
using GroceryCo.KioskSystem.DAL;
using GroceryCo.KioskSystem.DAL.DALDefinitions;
using log4net;

namespace GroceryCo.KioskSystem.Configuration
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<GroceryKiosk>().As<IGroceryKiosk>();
            builder.RegisterType<KioskDataStore>().As<IKioskDataStore>();
            builder.RegisterType<DALConfigurationManager>().As<IConfigurationManager>();
            builder.RegisterType<UserInput>().As<IUserInput>();
            builder.RegisterType<FormatOutput>().As<IFormatOutput>();
            builder.RegisterType<PromotionService>().As<IPromotionService>();
            builder.Register(l => LogManager.GetLogger(typeof(object))).As<ILog>();

            return builder.Build();
        }
    }
}
