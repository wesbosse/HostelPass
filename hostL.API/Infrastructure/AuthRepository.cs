using hostL.API.Infrastructure;
using hostL.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace hostL.API.Infrastructure
{
    public class AuthRepository : IDisposable
    {
        private HostLDataContext _hostLDataContext;
        private UserManager<HostLUser> _userManager;

        public AuthRepository()
        {
            _hostLDataContext = new HostLDataContext();
            var userStore = new UserStore<HostLUser> (_hostLDataContext);
            _userManager = new UserManager<HostLUser> (userStore);
        }

        public async Task<IdentityResult> RegisterUser(UserRegistrationModel model)
        {
            var user = new HostLUser
            {
                UserName = model.UserName,
                Email = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<HostLUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _hostLDataContext.Dispose();
            _userManager.Dispose();
        }
    }
}