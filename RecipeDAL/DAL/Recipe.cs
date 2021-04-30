using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeDAL.DAL

{
    public class Recipe: BaseDAO
    {
        public Recipe()
        {
            Ingridients = new HashSet<Ingridient>();
        }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
  
        public virtual HashSet<Ingridient> Ingridients { get; set; }

        [Required]
        [MaxLength(500)]

        public string Description { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Duration { get; set; }

        public int Status { get; set; }
        [Required]
       
        public int CategoryId { get; set; }
        public int EditedId { get; set; }
        public virtual Category Category { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
