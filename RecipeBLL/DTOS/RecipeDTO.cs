using RecipeBLL.Repository;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.DTOS
{
    public class RecipeDTO:BaseDTO
    {

        
        public string Name { get; set; }

        public virtual HashSet<Ingridient> Ingridients { get; set; }

       

        public string Description { get; set; }


        public int Status { get; set; }

        public string Photo { get; set; }

        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public virtual Category Category { get; set; }
        public int EditedId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
