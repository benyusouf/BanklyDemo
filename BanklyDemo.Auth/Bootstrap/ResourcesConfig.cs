using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace BanklyDemo.Auth.Bootstrap
{
    public static class ResourcesConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityresources() =>
            new List<IdentityResource> { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> { 
                new ApiResource("BanklyDemo", "A complaint service for BanklyDemo")
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> {
                new Client {
                    AllowedScopes = {
                        "BanklyDemo"
                    },
                    ClientId = "BanklyDemo_Swagger",
                    ClientSecrets = { new Secret("BanklyDemo_Secret".ToSha256())},
                    ClientName = "Swagger UI for BanklyDemo",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {"https://localhost:44327/oauth2-redirect.html"},
                    PostLogoutRedirectUris = { "https://localhost:44327/index/" },
                    RequireConsent = false
                },
                new Client {
                    AllowedScopes = {
                        "BanklyDemo",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },
                    ClientId = "BanklyDemo_Postman",
                    ClientSecrets = { new Secret("BanklyDemo_Postman_Secret".ToSha256())},
                    ClientName = "BanklyDemo calls from Postman",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    EnableLocalLogin = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    AllowedCorsOrigins = { "https://www.getpostman.com" },
                    RequireConsent = false
                }
            };
    }
}
