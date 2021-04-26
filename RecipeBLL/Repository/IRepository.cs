using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);


        void Create(TEntity entity);

        void Update(int id, TEntity entity);

        void Delete(int id);

    }

}
