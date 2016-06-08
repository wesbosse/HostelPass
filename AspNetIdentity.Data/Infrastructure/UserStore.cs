using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Infrastructure;
using AspNetIdentity.Data.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static System.String;

namespace AspNetIdentity.Data.Identity
{
    public class UserStore : Disposable, IUserPasswordStore<HostLUser, int>,
                             IUserSecurityStampStore<HostLUser, int>,
                             IUserRoleStore<HostLUser, int>
    {
        private readonly IDatabaseFactory _databaseFactory;

        private ApplicationDbContext _db;
        protected ApplicationDbContext Db
        {
            get
            {
                return _db ?? (_db = _databaseFactory.GetDataContext());
            }
        }

        public UserStore(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region IUserStore
        public virtual Task CreateAsync(HostLUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.Users.Add(user);
                Db.SaveChanges();
            });
        }

        public virtual Task DeleteAsync(HostLUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.Users.Remove(user);
                Db.SaveChanges();
            });
        }

        public virtual Task<HostLUser> FindByIdAsync(int userId)
        {
            return Task.Factory.StartNew(() => Db.Users.Find(userId));
        }

        public virtual Task<HostLUser> FindByNameAsync(string userName)
        {
            if (IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            return Task.Factory.StartNew(() =>
            {
                return Db.Users.FirstOrDefault(u => u.UserName == userName);
            });
        }

        public virtual Task UpdateAsync(HostLUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.Users.Attach(user);
                Db.Entry(user).State = EntityState.Modified;

                Db.SaveChanges();
            });
        }
        #endregion

        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(HostLUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(HostLUser user)
        {
            return Task.FromResult(!IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(HostLUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(HostLUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(HostLUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(HostLUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                if (!Db.Roles.Any(r => r.Name == roleName))
                {
                    Db.Roles.Add(new Role
                    {
                        Name = roleName
                    });
                }

                Db.UserRoles.Add(new UserRole
                {
                    Role = Db.Roles.FirstOrDefault(r => r.Name == roleName),
                    User = user
                });

                Db.SaveChanges();
            });
        }

        public Task RemoveFromRoleAsync(HostLUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                var userRole = user.Roles.FirstOrDefault(r => r.Role.Name == roleName);

                if (userRole == null)
                {
                    throw new InvalidOperationException("User does not have that role");
                }

                Db.UserRoles.Remove(userRole);

                Db.SaveChanges();
            });
        }

        public Task<IList<string>> GetRolesAsync(HostLUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                return (IList<string>)Db.Roles.Select(r => r.Name).ToList();
            });
        }

        public Task<bool> IsInRoleAsync(HostLUser user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                return user.Roles.Any(r => r.Role.Name == roleName);
            });
        }
        #endregion
    }
}
