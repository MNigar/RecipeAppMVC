using AutoMapper;
using RecipeApp.Filter;
using RecipeApp.Models;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Recipe;
using RecipeBLL.Repository.User;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RecipeApp.Controllers
{
  
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;

        private readonly RecipeContext _recipeContext;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _repository;

        public AdminController(ICategoryRepository repository, IMapper mapper, RecipeContext recipeContext, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _recipeContext = recipeContext;
            _userRepository = userRepository;

        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var check = _recipeContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();
                if (check != null)
                {
                    Session["email"] = check.Email;
                    Session["username"] = check.Name;
                    Session["userId"] = check.Id;

                    if (Crypto.VerifyHashedPassword(check.Password, user.Password))
                    {

                        if (Session["username"].ToString() == "admin")
                        {
                            return View("~/Areas/Manage/Views/Home/Index.cshtml");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Admin");
                        }

                    }

                }
                else
                {
                    Session["LoginError"] = true;


                    //ViewBag.Error = "Yoxdur";
                    return View("Login");
                }
            }
            return View();
        }
          
        }

    }
