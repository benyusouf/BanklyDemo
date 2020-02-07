using Microsoft.Extensions.DependencyInjection;

namespace BanklyDemo.UIApi.Bootstrap
{
    public static class AuthConfig
    {
        public static void AddAuthConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(config => {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookie").AddOpenIdConnect("oidc", config => {
                    config.Authority = "https://localhost:44333/";
                    config.ClientId = "BanklyDemo_swagger";
                    config.ClientSecret = "BanklyDemo_Secret";
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                });

            //services.AddAuthentication("Bearer").AddJwtBearer("Bearer", config => {
            //    config.Authority = "https://localhost:44333/";
            //    config.Audience = "BanklyDemo";
            //});
        }
    }
}
