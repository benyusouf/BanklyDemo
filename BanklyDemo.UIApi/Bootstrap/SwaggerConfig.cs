using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

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

                opt.AddSecurityDefinition("oauth2",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Description = "OAuth2 client credentials flow",
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("https://localhost:44333/connect/authorize"),
                                TokenUrl = new Uri("https://localhost:44333/connect/token"),
                                Scopes = new Dictionary<string, string>
                                {
                                    { "BanklyDemo", "BanklyDemo Api" }
                                }
                            }
                        }
                    });

                opt.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }


        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(SwaggerOpenAPISpecification, SwaggerOpenAPISpecificationDisplayName);
                x.RoutePrefix = string.Empty;
                x.OAuthClientId("BanklyDemo_Swagger");
                x.OAuthAppName("BanklyDemo - Swagger");
            });

            app.UseDeveloperExceptionPage();
        }
    }

    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            };

            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                        .Union(context.MethodInfo.GetCustomAttributes(true))
                                        .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {

                    },
                };

            }
        }
    }
}
