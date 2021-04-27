using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository
{
    public interface IRepository<TDTO> where TDTO : class

    {
        IQueryable<TDTO> GetAll();

        TDTO GetById(int id);


        void Create(TDTO entity,int userId);

        void Update(TDTO entity, int userId);

        void Delete(int id, int userId);

    }

}
