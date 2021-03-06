using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.DTOS
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public int ModifiedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
