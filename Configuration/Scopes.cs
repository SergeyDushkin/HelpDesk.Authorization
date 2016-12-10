using IdentityServer4.Models;
using System.Collections.Generic;

namespace authorization
{
    internal class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope> {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Roles,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "helpdesk",
                    DisplayName = "helpdesk api",
                    Description = "helpdesk api features and data",
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("name", alwaysInclude: true),
                        new ScopeClaim("given_name", alwaysInclude: true),
                        new ScopeClaim("family_name", alwaysInclude: true),
                        new ScopeClaim("email", alwaysInclude: true),
                        new ScopeClaim("role", alwaysInclude: true),
                        new ScopeClaim("website", alwaysInclude: true),
                    }
                }
            };
        }
    }
}
