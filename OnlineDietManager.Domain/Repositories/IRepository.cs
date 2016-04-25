using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Repositories
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
