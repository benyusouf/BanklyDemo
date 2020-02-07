using Autofac;
using System;
using System.Threading.Tasks;

namespace BanklyDemo.Core.Common
{
    public static class IocContainerProvider
    {
        public static IContainer Current { get; private set; }

        public static void Register(IContainer container)
        {
            Current = container;
        }

        public static void RunInLifetimeScope(Action<ILifetimeScope> operation, Action<ContainerBuilder> configurationAction = null)
        {
            if (configurationAction == null)
            {
                configurationAction = (ContainerBuilder b) => { };
            }

            using (var scope = Current.BeginLifetimeScope(configurationAction))
            {
                operation(scope);
            }
        }

        public static async Task RunInLifetimeScopeAsync(Func<ILifetimeScope, Task> operation,
            Action<ContainerBuilder> configurationAction = null, bool catchExceptions = false)
        {
            if (configurationAction == null)
            {
                configurationAction = (ContainerBuilder b) => { };
            }

            using (var scope = Current.BeginLifetimeScope(configurationAction))
            {
                try
                {
                    await operation(scope);
                }
                catch (Exception ex)
                {
                    if (!catchExceptions)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
