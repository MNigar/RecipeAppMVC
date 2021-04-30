using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.DTOS
{
    public class UserDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int Status { get; set; }
        public virtual HashSet<Category> Categories { get; set; }
        public virtual HashSet<Recipe> Recipes { get; set; }

    }
}
