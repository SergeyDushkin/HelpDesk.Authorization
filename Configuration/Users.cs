using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace authorization
{
    internal class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser> {
                new InMemoryUser {
                    Subject = "818727",
                    Username = "alice",
                    Password = "password",
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "user"),
                        new Claim(JwtClaimTypes.Role, "auditor"),
                        new Claim(JwtClaimTypes.Role, "priceservice.user"),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new InMemoryUser {
                    Subject = "88421113",
                    Username = "bob",
                    Password = "password",
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.Role, "auditor"),
                        new Claim(JwtClaimTypes.Role, "priceservice.user"),
                        new Claim(JwtClaimTypes.Role, "CLIENT"),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };
        }
    }
}
