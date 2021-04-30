using AutoMapper;
using AutoMapper.QueryableExtensions;
using RecipeBLL.DTOS;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecipeBLL.Repository
{
   public class RepositoryClass<TDto, TDao> : IRepository<TDto> where TDto :  BaseDTO
        where TDao : BaseDAO
    {
        private readonly RecipeContext _dbContext;
        private readonly IMapper _mapper;

        public RepositoryClass(RecipeContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //RecipeContext _dbContext = new RecipeContext();
        public void Create(TDto entity,int userId=0)
        {
            try
            {
                var model = _mapper.Map<TDao>(entity);
                model.CreatedUserId = (HttpContext.Current.Session["userId"]==null)?userId: (int)HttpContext.Current.Session["userId"];
                model.ModifiedUserId = (HttpContext.Current.Session["userId"] == null)?userId: (int)HttpContext.Current.Session["userId"];
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Set<TDao>().Add(model);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                   
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Delete(int id,int userId=0)
        {
            //var entity = _dbContext.Set<TEntity>().Find(id);
            //_dbContext.Set<TEntity>().Remove(entity);
            //_dbContext.SaveChanges();
            try
            {   
                var dao = _dbContext.Set<TDao>().FirstOrDefault(e => e.Id == id);
                _dbContext.Entry(dao).State = EntityState.Deleted;
                var result = _dbContext.SaveChanges();
               
            }
            catch (Exception ex)
            {
                
            }
        }

        public IQueryable<TDto> GetAll()
        {
            //return _dbContext.Set<TEntity>();
            
              
                var entities = _dbContext.Set<TDao>().ProjectTo<TDto>(_mapper.ConfigurationProvider);
                var list = entities.ToList().AsQueryable();
                //var entitiess = ctx.Set<TDao>();
                //var t = _mapper.Map<IQueryable<TDto>>(entitiess);
                return list;

            
        }

        public TDto GetById(int id)
        {
           
            var dao = _dbContext.Set<TDao>().FirstOrDefault(e => e.Id == id);
            var dto = _mapper.Map<TDto>(dao);
            return dto;
        }

        
        public void Update( TDto entity,int userId=0  )
        {
           
            try
            {
                var model = _mapper.Map<TDao>(entity);

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var dbModel = _dbContext.Set<TDao>().FirstOrDefault(x => x.Id == model.Id);
                    dbModel.ModifiedUserId = (int)HttpContext.Current.Session["userId"]; ;
                    dbModel.ModifiedDate = DateTime.Now;
                    var entry = _dbContext.Entry(dbModel);
                    entry.CurrentValues.SetValues(entity);
                    entry.Property(x => x.CreatedDate).IsModified = false;
                    entry.Property(x => x.CreatedUserId).IsModified = false;
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    
             
                }

            }
            catch (Exception ex)
            {
                
            }
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
