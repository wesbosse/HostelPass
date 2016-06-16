using HostLPass.Core.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HostLPass.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private Func<IAuthorizationRepository> _authRepositoryFactory;
        private IAuthorizationRepository _authorizationRepository
        {
            get
            {
                return _authRepositoryFactory.Invoke();
            }
        }

        public CustomOAuthProvider(Func<IAuthorizationRepository> authRepositoryFactory)
        {
            _authRepositoryFactory = authRepositoryFactory;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var user = await _authorizationRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var token = new ClaimsIdentity(context.Options.AuthenticationType);
            token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            foreach (var role in user.Roles)
            {
                token.AddClaim(new Claim(ClaimTypes.Role, role.Role.Name));
            }

            var authenticationProperties = new AuthenticationProperties(new Dictionary<string, string>()
            {
                { "username", user.UserName },
                { "hostelOwner", user.HostelOwner.ToString() },
                { "roles", user.Roles.Count.ToString() },
/*                { "name", $"{user.FirstName} {user.LastName}" },
/*                { "email", user.EmailAddress },#1#
                { "messages", user.Messages.Count.ToString() },
                { "rating", user.Ratings.Average(r => r.AverageRating).ToString() }  */              
            });
           
            var ticket = new AuthenticationTicket(token, authenticationProperties);
            
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