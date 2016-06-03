using hostL.API.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Models
{
    public class HostLUserManager : UserManager<HostLUser>
    {
        public HostLUserManager(IUserStore<HostLUser> store)
            : base(store)
        {
        }

        public static HostLUserManager Create(IdentityFactoryOptions<HostLUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<HostLDataContext>();
            var HostLUserManager = new HostLUserManager(new UserStore<HostLUser>(appDbContext));

            return HostLUserManager;
        }
    }
}