using AspNetIdentity.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly ApplicationDbContext _dataContext;

        public ApplicationDbContext GetDataContext()
        {
            return _dataContext ?? new ApplicationDbContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new ApplicationDbContext();
        }

        protected override void DisposeCore()
        {
            _dataContext.Dispose();
        }
    }
}
