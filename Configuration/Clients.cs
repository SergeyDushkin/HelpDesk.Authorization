using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace authorization
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            var clients = new List<Client>();

            // resource owner password grant client
            clients.Add(new Client
            {
                ClientId = "public",
                ClientName = "Public Resource Owner Client",
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new List<string>
                {
                    "offline_access", "helpdesk"
                }
            });

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Role, "OPERATOR"));

            // Telegram Client
            clients.Add(new Client
            {
                ClientId = "telegram",
                ClientName = "Telegram Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret("telegram123456789".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    "helpdesk"
                },
                Claims = claims
            });

            return clients;
        }
    }
}
