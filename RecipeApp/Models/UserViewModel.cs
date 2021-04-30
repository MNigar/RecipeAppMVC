using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeApp.Models
{
    public class UserViewModel
    {

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