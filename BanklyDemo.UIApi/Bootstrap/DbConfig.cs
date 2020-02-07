using BanklyDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BanklyDemo.UIApi.Bootstrap
{
    public static class DbConfig
    {
        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BanklyDemoDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                       builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)
                        ));
        }
    }
}
