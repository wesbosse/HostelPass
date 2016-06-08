using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected IDatabaseFactory DatabaseFactory { get; set; }
        protected IDbSet<TEntity> DbSet { get; set; }

        private ApplicationDbContext _dataContext;
        protected ApplicationDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.GetDataContext()); }
        }

        protected Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;

            DbSet = DataContext.Set<TEntity>();
        }

        // CREATE
        public virtual TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        // READ
        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.FirstOrDefault(where);
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where);
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual int Count(Func<TEntity, bool> where)
        {
            return DbSet.Count(where);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Any(where);
        }

        // UPDATE
        public virtual void Update(TEntity entity)
        {
            var t = DbSet.Attach(entity);

            DataContext.Entry(entity).State = EntityState.Modified;
        }

        // DELETE
        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
        public virtual void Delete(object id)
        {
            var entity = GetById(id);

            Delete(entity);
        }
    }
}
