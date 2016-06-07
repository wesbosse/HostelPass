using AspNetIdentity.WebApi.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AspNetIdentity.WebApi.Models;

namespace AspNetIdentity.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var roleManager = context.OwinContext.GetUserManager<ApplicationRoleManager>();

            HostLUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("invalid_grant", "User did not confirm email.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

            //TODO: Potential bug - not sure if roleManager.Roles gets the roles from the DB..
            string[] potentialRoles = roleManager.Roles.Select(r => r.Name).ToArray();

            foreach (var potentialRole in potentialRoles)
            {
                if (userManager.IsInRole(user.Id, potentialRole))
                {
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, potentialRole));
                }
            }

            var authenticationProperties = new AuthenticationProperties(new Dictionary<string, string>()
            {
                { "username", user.UserName },
                /*{ "name", $"{user.FirstName} {user.LastName}" },
                { "email", user.Email },
                { "messages", user.Messages.Count.ToString() },
                { "rating", user.Ratings.Average(r => r.AverageRating).ToString() }      */          
            });
           
            var ticket = new AuthenticationTicket(oAuthIdentity, authenticationProperties);
            
            context.Validated(ticket);
           
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