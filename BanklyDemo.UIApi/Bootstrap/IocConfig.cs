using Autofac;
using Autofac.Extensions.DependencyInjection;
using BanklyDemo.Core.Common;
using BanklyDemo.Data.Bootstrap;
using BanklyDemo.DomainServices.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BanklyDemo.UIApi.Bootstrap
{
    public static class IocConfig
    {
        public static IServiceProvider AddDependencies(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            RegisterDependencies(containerBuilder);

            services.AddMemoryCache();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            IocContainerProvider.Register(container);

            return new AutofacServiceProvider(container);
        }

        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAutofacModule>();
            builder.RegisterModule<DomainServicesAutofacModule>();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        }
    }
}
