using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeApp.Models
{
    public class LogoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}