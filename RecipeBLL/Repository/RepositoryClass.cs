using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository
{
   public class RepositoryClass<TEntity> : IRepository<TEntity> where TEntity :  BaseDAO
    {
        private readonly RecipeContext _dbContext;

        public RepositoryClass(RecipeContext dbContext)
        {
            _dbContext = dbContext;
        }
        //RecipeContext _dbContext = new RecipeContext();
        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate).ToList();
        }
        public void Update(int id, TEntity entity)
        {
            //var data = _dbContext.Entry(entity);
            //data.State = System.Data.Entity.EntityState.Modified;
            //_dbContext.SaveChanges();
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

        }
    }

}
