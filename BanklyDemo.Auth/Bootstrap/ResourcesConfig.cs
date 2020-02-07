using IdentityModel;
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
                new ApiResource("BanklyDemo"),
                new ApiResource("BanklyDemo2")
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> {
                new Client
                {
                    AllowedScopes = { "BanklyDemo" },
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                },
                new Client
                {
                    AllowedScopes = { 
                        "BanklyDemo", 
                        "BanklyDemo2", 
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile
                    },
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:44305/signin-oidc"},
                    RequireConsent = false
                },
                new Client {
                    AllowedScopes = {
                        "BanklyDemo",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile
                    },
                    ClientId = "BanklyDemo_swagger",
                    ClientSecrets = { new Secret("BanklyDemo_Secret".ToSha256())},
                    ClientName = "Swagger UI for BanklyDemo",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {"https://localhost:44305/signin-oidc"},
                    RequireConsent = false
                }
            };
    }
}
