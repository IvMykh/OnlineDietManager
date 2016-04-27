using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.Repositories;

namespace OnlineDietManager.WebUI.Repositories
{
    //public class GenericRepository<TEntity>
    //    : IRepository<TEntity>
    //      where TEntity : class
    //{
    //    private OnlineDietManagerContext _context;
    //    private DbSet<TEntity> _dbSet;

    //    public GenericRepository(OnlineDietManagerContext context)
    //    {
    //        _context = context;
    //        _dbSet = context.Set<TEntity>();
    //    }

    //    public IQueryable<TEntity> GetAll()
    //    {
    //        return this._dbSet.AsQueryable();
    //    }

    //    public virtual TEntity GetById(params Object[] id)
    //    {
    //        return this._dbSet.Find(id);
    //    }


    //    public virtual void Insert(TEntity entity)
    //    {
    //        this._dbSet.Add(entity);
    //    }

    //    public virtual void Delete(params Object[] id)
    //    {
    //        var entityToDelete = this._dbSet.Find(id);
    //        this.Delete(entityToDelete);
    //    }

    //    public virtual void Delete(TEntity entityToDelete)
    //    {
    //        if (this._context.Entry(entityToDelete).State == EntityState.Detached)
    //        {
    //            this._dbSet.Attach(entityToDelete);
    //        }
    //        this._dbSet.Remove(entityToDelete);
    //    }

    //    public virtual void Update(TEntity entityToUpdate)
    //    {
    //        this._dbSet.Attach(entityToUpdate);
    //        this._context.Entry(entityToUpdate).State = EntityState.Modified;
    //    }
    //}
}