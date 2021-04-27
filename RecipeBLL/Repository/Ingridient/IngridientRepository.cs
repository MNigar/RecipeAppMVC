using AutoMapper;
using RecipeBLL.DTOS;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository.Ingridient
{
    public class IngridientRepository : RepositoryClass<IngridientDTO, RecipeDAL.DAL.Ingridient>, IIngridientRepository
    {
        public IngridientRepository(RecipeContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
