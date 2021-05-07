using AutoMapper;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Areas.Manage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _repository;
       
        private readonly RecipeContext _recipeContext;
        private readonly IMapper _mapper;
        public HomeController(IRecipeRepository repository, IMapper mapper, RecipeContext recipeContext)
        {
            _mapper = mapper;
            _repository = repository;
          
            _recipeContext = recipeContext;
        }
        // GET: Manage/Recipe
        // GET: Manage/Home
        public ActionResult Index()
        {
           
            if (Session["username"] != null)
            {
                
                if (Session["username"].ToString() == "admin")
                {
                    if (_repository.GetAll().Where(x => x.Status == (int)Helper.Helpers.status.Waiting).Count() > 0)
                    {
                        ViewBag.Not = "Yeni reseptler daxil edilib";
                    }
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