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
                new IdentityResources.OpenId()
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api", "Around API")
            };


        public static IEnumerable<Client> Clients(List<string> spaRedirectUris,
            List<string> spaPostLogoutRedirectUris, List<string> spaAllowedCorsOrigins) =>
            new Client[]
            {
                new Client
                {
                    ClientId = "around-client",
                    ClientName = "Around Client",
                    ClientUri = "https://mvp-stack.com",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    RedirectUris = spaRedirectUris,
                    PostLogoutRedirectUris = spaPostLogoutRedirectUris,
                    AllowedCorsOrigins = spaAllowedCorsOrigins,

                    EnableLocalLogin = false,
                    AllowOfflineAccess = true,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api"
                    }
                }
            };
    }
}