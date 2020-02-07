using Autofac;
using System.Linq;

namespace BanklyDemo.AuthData.Bootstrap
{
    public class AuthDataAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Repository")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Repository")))
                .InstancePerLifetimeScope();
        }
    }
}
