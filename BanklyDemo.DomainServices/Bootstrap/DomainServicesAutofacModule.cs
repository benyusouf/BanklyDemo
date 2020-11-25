using Autofac;
using BanklyDemo.Core.Common;
using BanklyDemo.DomainServices.Crypto;
using System.Linq;

namespace BanklyDemo.DomainServices.Bootstrap
{
    public class DomainServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Service")))
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .InstancePerLifetimeScope();

        }
    }
}
