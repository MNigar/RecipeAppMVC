using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApp.Models.ViewModels
{
    public class Mail
    {public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }

     
    }
}