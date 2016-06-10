using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Repository;
using AspNetIdentity.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Data.Repository
{
    public class HostLUserRepository : Repository<HostLUser>, IHostLUserRepository
    {
        public HostLUserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
