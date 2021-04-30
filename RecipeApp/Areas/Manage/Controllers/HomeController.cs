using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Areas.Manage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Manage/Home
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                if (Session["username"].ToString() == "admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Admin", new { area = "" });

                }
            }
            else
            {
                return RedirectToAction("Login", "Admin", new { area = "" });
            }
           
        }
    }
}