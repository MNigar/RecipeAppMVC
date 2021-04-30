using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDAL.DAL
{
    public class User : BaseDAO
    {
        public User()
        {
            Categories = new HashSet<Category>();
            Recipes = new HashSet<Recipe>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
       
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public int Status { get; set; }
        public virtual HashSet<Category> Categories { get; set; }
        public virtual HashSet<Recipe> Recipes { get; set; }
    }
}
