﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace MCA.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("msascope", "MSA Server"),
                    new ApiScope(name:"read", displayName: "Read your data."),
                    new ApiScope(name:"write", displayName: "Write your data."),
                    new ApiScope(name:"delete", displayName: "Delete your data.")
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "read","write","profile"}
                },
                new Client
                {
                    ClientId="msa",
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Code,
                    RedirectUris={ "https://localhost:4200/signin-oidc","https://localhost:44326/signin-oidc","https://localhost:7081/signin-oidc","http://localhost:5081/signin-oidc"},
                    PostLogoutRedirectUris={ "https://localhost:44326/signout-callback-oidc"},
                    AllowedScopes= new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "msascope"
                    }
                }
            };
    }
}
