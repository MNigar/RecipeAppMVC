using RecipeBLL.DTOS;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository.Recipe
{
    public class RecipeRepository : RepositoryClass<RecipeDAL.DAL.Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeContext dbContext) : base(dbContext)
        {

        }


    }

}
