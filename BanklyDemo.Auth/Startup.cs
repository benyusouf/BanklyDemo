using AutoMapper;
using BanklyDemo.Auth.Bootstrap;
using BanklyDemo.AuthData;
using BanklyDemo.AuthData.Repositories;
using BanklyDemo.Core.Data;
using BanklyDemo.Core.Users.Models;
using BanklyDemo.DomainServices.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BanklyDemo.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"),
                        builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));


            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "BanklyDemo.Auth.Cookie";
                config.LoginPath = "/Account/Login";
            });


            services.AddControllersWithViews();
            services.AddIdentityServer()
                .AddInMemoryApiResources(ResourcesConfig.GetApis())
                .AddInMemoryClients(ResourcesConfig.GetClients())
                .AddInMemoryIdentityResources(ResourcesConfig.GetIdentityresources())
                .AddDeveloperSigningCredential();

            Mapper.Initialize(config => {
                config.AddProfile<DomainServicesMapperProfile>();
            });

            services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services.AddDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseCors(
                    options => options.WithOrigins("https://localhost:44327/").AllowAnyMethod()
                );


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
