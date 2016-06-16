using HostLPass.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostLPass.Core.Domain;
using HostLPass.Core.Models;
using Microsoft.AspNet.Identity;

namespace HostLPass.Data.Infrastructure
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
                HostelOwner = registration.HostelOwner,
                EmailAddress = registration.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, registration.Password);

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

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
