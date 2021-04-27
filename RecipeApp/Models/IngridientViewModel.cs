using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeApp.Models
{
    public class IngridientViewModel
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        [Required]
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}