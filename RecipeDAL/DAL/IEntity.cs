using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDAL.DAL
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }

        int CreatedUserId { get; set; }
        int ModifiedUserId { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
