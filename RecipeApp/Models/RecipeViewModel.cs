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

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual HashSet<Ingridient> Ingridients { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(15)]
        public string Description { get; set; }
        public int EditedId { get; set; }


        public int Status { get; set; }
        [Required]

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int UserId { get; set; }
       
        public virtual User User { get; set; }
        public string Photo { get; set; }
        public string Duration { get; set; }
    }
}