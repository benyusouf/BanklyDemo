using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;

namespace BanklyDemo.UIApi.Bootstrap
{
    public static class AuthConfig
    {
        public static void AddAuthConfiguration(this IServiceCollection services)
        {

            services.AddAuthorization();

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options => {
                options.Authority = "https://localhost:44333/";
                options.ApiName = "BanklyDemo";
                options.RequireHttpsMetadata = false;
                options.EnableCaching = true;
                options.CacheDuration = TimeSpan.FromMinutes(2);
            });


        }
    }
}
