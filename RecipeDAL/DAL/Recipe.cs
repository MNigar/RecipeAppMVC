using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecipeDAL.DAL

{
    public class Recipe: BaseDAO
    {
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
