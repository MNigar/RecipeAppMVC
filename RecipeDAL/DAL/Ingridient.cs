﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDAL.DAL
{
    public class Ingridient:BaseDAO
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        [Required]
        public string Name { get; set; }
        public string Quantity { get; set; }

    }
}
