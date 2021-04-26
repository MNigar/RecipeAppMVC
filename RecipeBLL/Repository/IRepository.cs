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


        void Create(TEntity entity,int userId);

        void Update( TEntity entity, int userId);

        void Delete(int id);

    }

}
