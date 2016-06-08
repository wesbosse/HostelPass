using AspNetIdentity.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Models;
using Microsoft.AspNet.Identity;

namespace AspNetIdentity.Data.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IUserStore<HostLUser, int> _userStore;
        private readonly UserManager<HostLUser, int> _userManager;

        public AuthorizationRepository(IUserStore<HostLUser, int> userStore)
        {
            _userStore = userStore;
            _userManager = new UserManager<HostLUser, int>(userStore);
        }

        public async Task<HostLUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }
  
        public async Task<IdentityResult> Register(CreateUserBindingModel registration)
        {
            var user = new HostLUser
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                JoinDate = DateTime.Now,
                UserName = registration.Username,
                HostelOwner = registration.HostelOwner
            };

            var result = await _userManager.CreateAsync(user, registration.Password);

            //TODO: Add the Hostel role to this user
            if(user.HostelOwner)
            {
                _userManager.AddToRole(user.Id, "HostelOwner");
            }
            else
            {
                _userManager.AddToRole(user.Id, "Traveller");
            }

            return result;
        }

        public async Task<IdentityResult> RegisterTraveller(UserRegistrationModel registration)
        {
            var user = new HostLUser
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                JoinDate = DateTime.Now,
                UserName = registration.UserName,
                HostelOwner = registration.HostelOwner
            };

            var result = await _userManager.CreateAsync(user, registration.Password);

            //TODO: Add the Traveller role to this user

            return result;
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
