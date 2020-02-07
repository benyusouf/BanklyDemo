using Autofac;
using Autofac.Extensions.DependencyInjection;
using BanklyDemo.AuthData.Bootstrap;
using BanklyDemo.Core.Common;
using BanklyDemo.DomainServices.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BanklyDemo.Auth.Bootstrap
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
            builder.RegisterModule<AuthDataAutofacModule>();
            builder.RegisterModule<DomainServicesAutofacModule>();
        }
    }
}
