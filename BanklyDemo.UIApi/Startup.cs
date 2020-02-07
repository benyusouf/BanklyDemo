using AutoMapper;
using BanklyDemo.DomainServices.Bootstrap;
using BanklyDemo.UIApi.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BanklyDemo.UIApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        const string DefaultCorsPolicy = "Cors-AllowAll";

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextConfiguration(Configuration);

            services.AddControllers();

            services.AddCors(); 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);



            services.AddAuthConfiguration();
            //services.AddAuthentication("Bearer").AddJwtBearer("Bearer", config => {
            //    config.Authority = "https://localhost:44333/";
            //    config.Audience = "BanklyDemo";
            //});

            Mapper.Initialize(config => {
                config.AddProfile<DomainServicesMapperProfile>();
            });

            services.AddSwaggerConfiguration();

            return services.AddDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerConfiguration();

            app.UseCors(
                options => options.WithOrigins("https://localhost:44333/").AllowAnyMethod()
            );


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
