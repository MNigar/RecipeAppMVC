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
        public virtual List<Recipe> Recipes { get; set; }
        public virtual List<Category> Categories { get; set; }


    }
}