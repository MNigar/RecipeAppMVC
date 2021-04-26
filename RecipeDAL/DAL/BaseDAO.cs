using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDAL.DAL
{
   public  class BaseDAO
    {
        [Key]
        public int Id { get; set; }

        public int CreatedUserId { get; set; }
        public int ModifiedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
