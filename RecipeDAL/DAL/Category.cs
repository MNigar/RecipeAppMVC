using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDAL.DAL
{
   public  class Category:BaseDAO
    {
        public Category()
        {
            Recipes = new HashSet<Recipe>();
        }

        public string Name { get; set; }
        public virtual HashSet<Recipe> Recipes { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
