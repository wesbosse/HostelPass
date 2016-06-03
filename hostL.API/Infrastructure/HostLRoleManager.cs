using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Infrastructure
{
    public class HostLRoleManager : RoleManager<IdentityRole>
    {
        public HostLRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static HostLRoleManager Create(IdentityFactoryOptions<HostLRoleManager> options, IOwinContext context)
        {
            var HostLRoleManager = new HostLRoleManager(new RoleStore<IdentityRole>(context.Get<HostLDataContext>()));

            return HostLRoleManager;
        }
    }
}