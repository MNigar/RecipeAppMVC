using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual HashSet<Recipe> Recipes { get; set; }
        public int UserId { get; set; }
        public virtual User UserDAO { get; set; }
    }
}