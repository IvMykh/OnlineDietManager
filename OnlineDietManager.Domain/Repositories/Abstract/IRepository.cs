using System;
using System.Linq;

namespace OnlineDietManager.Domain.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(params Object[] id);

        void Insert(TEntity entity);
        void Delete(params Object[] id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
