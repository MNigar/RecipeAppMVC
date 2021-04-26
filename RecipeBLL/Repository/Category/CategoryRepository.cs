using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository.Category
{
   public class CategoryRepository : RepositoryClass<RecipeDAL.DAL.Category>, ICategoryRepository
    {
        public CategoryRepository(RecipeContext dbContext) : base(dbContext)
        {

        }

    }
}
