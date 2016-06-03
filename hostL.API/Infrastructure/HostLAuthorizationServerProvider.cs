using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace hostL.API.Infrastructure
{
    public class HostLAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // 1. Allow cors
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            // Create Cors user
            using (var authRepository = new AuthRepository())
            {
                var user = await authRepository.FindUser(
                    context.UserName,
                    context.Password
                );

                if (user == null)
                {
                    context.SetError("invalid_grant", "Username or password is incorrect");

                    return;
                }
                else
                {
                    var token = new ClaimsIdentity(context.Options.AuthenticationType);
                    token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    if(user.HostelOwner == false)
                    { 
                        token.AddClaim(new Claim(ClaimTypes.Role, "Traveller"));
                    }
                    else
                    {
                        token.AddClaim(new Claim(ClaimTypes.Role, "HostelOwner"));
                    }

                    context.Validated(token);
                }


            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}