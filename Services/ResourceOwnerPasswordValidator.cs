﻿using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace authorization
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IProfileManager manager;
        public ResourceOwnerPasswordValidator(IProfileManager manager)
        {
            this.manager = manager;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = this.manager.Find(context.UserName, context.Password);

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Username Or Password Incorrect"); ;
                return Task.FromResult(0);
            }

            var claims = new List<Claim> {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.GivenName, ""),
                new Claim(JwtClaimTypes.FamilyName, ""),
                new Claim(JwtClaimTypes.Email, ""),
                new Claim(JwtClaimTypes.PhoneNumber, ""),
                new Claim(JwtClaimTypes.Id, user.Id.ToString()),
            };

            var roles = user.GetClaims().Select(r => new Claim(r.Type, r.Value));
            
            claims.AddRange(roles);

            context.Result = new GrantValidationResult(subject: user.Id.ToString(), authenticationMethod: "Bearer", claims: claims); ;

            return Task.FromResult(0);
        }
    }
}
