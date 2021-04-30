using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Filter
{
    public class AuthForAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["email"] == null)
            {
                filterContext.Result = new RedirectResult("~/admin");

            }

            base.OnActionExecuting(filterContext);
        }
    }
}