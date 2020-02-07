using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace BanklyDemo.UIApi.Bootstrap
{
    public static class SwaggerConfig
    {
        private const string SwaggerOpenAPISpecification = "/swagger/v1/swagger.json";
        private const string SwaggerOpenAPISpecificationDisplayName = "BanklyDemo API";

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BanklyDemo API",
                    Version = "v1",
                    Description = "A complaint service API",
                    Contact = new OpenApiContact
                    {
                        Name = "Bankly NG",
                        Email = "hey@bankly.ng"
                    }
                });

                opt.AddSecurityDefinition("auth2_security",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Description = "OAuth2 client credentials flow",
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("https://localhost:44333/Account/Login"),
                                Scopes = new Dictionary<string, string>
                                {
                                    { "BanklyDemo", "BanklyDemo Api" }
                                }
                            }
                        }
                    });
            });
        }


        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(SwaggerOpenAPISpecification, SwaggerOpenAPISpecificationDisplayName);
                x.RoutePrefix = string.Empty;
                x.OAuthClientId("BanklyDemo_swagger");
                x.OAuthAppName("BanklyDemo - Swagger");
            });

            app.UseDeveloperExceptionPage();
        }
    }
}
