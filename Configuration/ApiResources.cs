using IdentityServer4.Models;
using System.Collections.Generic;

namespace authorization
{
    internal class ApiResources
    {
        public static IEnumerable<ApiResource> Get()
        {
            return new List<ApiResource>
            {
                new ApiResource("helpdesk", "helpdesk api") {
                    
                    Description = "helpdesk api features and data",
                    UserClaims = new [] { "name", "given_name", "family_name", "email", "role", "website" }
                }
            };
        }
    }
}
