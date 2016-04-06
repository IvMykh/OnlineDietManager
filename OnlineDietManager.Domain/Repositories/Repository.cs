using System;
using System.Data.Entity;
using System.Linq;
using OnlineDietManager.Domain.Practice;
using OnlineDietManager.Domain.Repositories.Abstract;

namespace OnlineDietManager.Domain.Repositories
{
    public class Repository<TEntity>
        : IRepository<TEntity>
          where TEntity: class
    {
        private MyOnlineDietManagerContext  _context;
        private DbSet<TEntity>              _dbSet;

        public Repository(MyOnlineDietManagerContext context)
        {
            _context    = context;
            _dbSet      = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return this._dbSet.AsQueryable();
        }

        public virtual TEntity GetById(params Object[] id)
        {
            return this._dbSet.Find(id);
        }


        public virtual void Insert(TEntity entity)
        {
            this._dbSet.Add(entity);
        }

        public virtual void Delete(params Object[] id)
        {
            var entityToDelete = this._dbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this._context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this._dbSet.Attach(entityToDelete);
            }
            this._dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this._dbSet.Attach(entityToUpdate);
            this._context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
