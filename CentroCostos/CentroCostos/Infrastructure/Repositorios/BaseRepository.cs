using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    // http://blog.gauffin.org/2013/01/repository-pattern-done-right/
    public class BaseRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ApplicationContext _dbContext;

        public BaseRepository(ApplicationContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        protected ApplicationContext DbContext
        {
            get { return _dbContext; }
        }

        public void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Add(entity);
        }

        public void CreateMultiple(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public TEntity GetById(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        
        public IEnumerable<TEntity> FindAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }
    }
}