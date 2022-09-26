using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Identity
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApis()
            => new List<ApiResource>
            {
                new ApiResource("Api_1")
                {
                    Scopes = {"Api_1"}
                },
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = {new Secret ("super_hard_to_guess".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"Api_1"}
                }
            };

        public static IEnumerable<ApiScope> GetScopes()
            => new List<ApiScope>
            {
                new ApiScope("Api_1")
            };

    }
}
