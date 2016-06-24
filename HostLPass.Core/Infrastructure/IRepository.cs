using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HostLPass.Core.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // CREATE
        TEntity Add(TEntity entity);

        // READ
        TEntity GetById(object id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where);
        int Count();
        int Count(Func<TEntity, bool> where);
        bool Any(Expression<Func<TEntity, bool>> where);

        // UPDATE
        void Update(TEntity entity);

        // DELETE
        void Delete(object id);
        void Delete(TEntity entity);
    }
}
