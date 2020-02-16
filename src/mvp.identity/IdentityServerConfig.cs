// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace mvp.identity
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api", "Around API")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                //new Client
                //{
                //    ClientId = "client",
                //    ClientName = "Client Credentials Client",

                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                //    AllowedScopes = { "api" }
                //},

                // MVC client using code flow + pkce
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientName = "MVC Client",

                //    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                //    RequirePkce = true,
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    RedirectUris = { "http://localhost:5003/signin-oidc" },
                //    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                //    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "api" }
                //},

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "around-client",
                    ClientName = "Around Client",
                    ClientUri = "https://mvp-stack.com",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    RedirectUris =
                    {
                        "http://localhost:4200/callback.html",
                        "http://localhost:4200/renew-callback.htmll",
                        "https://mvp-stack.com/callback.html",
                        "https://mvp-stack.com/renew-callback.htmll"
                    },

                    PostLogoutRedirectUris = {
                        "http://localhost:4200/signout-callback.html",
                        "https://mvp-stack.com/signout-callback.html"
                    },
                    AllowedCorsOrigins = {
                        "http://localhost:4200",
                        "https://mvp-stack.com"
                    },
                    EnableLocalLogin = false,
                    AllowOfflineAccess = true,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api"
                    }
                }
            };
    }
}