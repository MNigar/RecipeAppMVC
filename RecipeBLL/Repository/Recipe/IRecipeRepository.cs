using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBLL.DTOS;
using RecipeDAL.DAL;
namespace RecipeBLL.Repository.Recipe
{
   public interface IRecipeRepository:IRepository<Recipes>
    {
    }
}
