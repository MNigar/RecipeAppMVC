using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApp.Models.ViewModels
{
    public class RecipeIngridientViewModel
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public RecipeViewModel RecipeViewModel { get; set; }
        public IngridientList IngridientViewModel { get; set; }


    }
}