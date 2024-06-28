﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace EShopping.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalogapi"),
                new ApiScope("basketapi"),
                new ApiScope("catalogapi.read"),
                new ApiScope("catalogapi.write"),
                new ApiScope("eshoppinggateway")
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
               //List of microservices
               new ApiResource("Catalog", "Catalog.API")
               {
                   Scopes = {"catalogapi.read","catalogapi.write" }
               },
               new ApiResource("Basket", "Basket.API")
                {
                     Scopes = {"basketapi" }
                },
               new ApiResource("EShoppingGateway", "EShopping Gateway")
                {
                     Scopes = {"eshoppinggateway" }
                }

            };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //m2m flow
                new Client
                {
                    ClientName = "Catalog API Client",
                    ClientId = "CatalogApiClient",
                    ClientSecrets = {new Secret("c4f1d8a0-7b3e-4b4a-9e4c-6f8a2d3b5e0f".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalogapi.read","catalogapi.write" }

                },
                 new Client
                {
                    ClientName = "Basket API Client",
                    ClientId = "BasketApiClient",
                    ClientSecrets = {new Secret("f47ab4c5-58cc-4372-a567-0e02b2c3d479".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "basketapi" }

                },
                  new Client
                {
                    ClientName = "EShopping Gateway Client",
                    ClientId = "EShoppingGatewayClient",
                    ClientSecrets = {new Secret("f48ab4c5-58cc-4372-a567-0e02b2c3d479".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "eshoppinggateway","basketapi" }

                }
            };
    }
}