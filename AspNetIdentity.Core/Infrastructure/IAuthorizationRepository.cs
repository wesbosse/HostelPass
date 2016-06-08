using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Core.Infrastructure
{
    public interface IAuthorizationRepository : IDisposable
    {
        Task<IdentityResult> Register(CreateUserBindingModel registration);
        Task<HostLUser> FindUser(string username, string password);
    }
}
