using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.DTOS
{
   public class CategoryDTO:BaseDTO
    {
        public string Name { get; set; }
        
        public int UserId { get; set; }
        public string Photo { get; set; }

        public virtual User User { get; set; }
    }
}
