using AutoMapper;
using RecipeBLL.DTOS;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository.Category
{
   public class CategoryRepository : RepositoryClass<CategoryDTO,RecipeDAL.DAL.Category>, ICategoryRepository
    {
        public CategoryRepository(RecipeContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {

        }

     
    }
}
