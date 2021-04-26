using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeApp.Models
{
    public class RecipeViewModel
    {   

       public int Id { get; set; }

        [MaxLength(200)]
        public string Ingridients { get; set; }


        public string Quantity { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        public int Status { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}