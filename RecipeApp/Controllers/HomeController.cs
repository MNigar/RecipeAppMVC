using AutoMapper;
using RecipeApp.Helper;
using RecipeApp.Models;
using RecipeApp.Models.ViewModels;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Recipe;
using RecipeBLL.Repository.User;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RecipeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;


        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        private readonly RecipeContext _recipeContext;
        private readonly IUserRepository _userRepository;
        public HomeController(IRecipeRepository repository, IMapper mapper, ICategoryRepository catrepository, IIngridientRepository ingridientrepository, RecipeContext recipeContext, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
            _userRepository = userRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult Profiles()
        {
            if (Session["username"] == null)
            {
                TempData["Message"] = "Please login before submit recipe";
                return RedirectToAction("Login", "Home");
            }
            var data = _repository.GetAll().Where(x=> x.CreatedUserId==(int)Session["userId"]);
            var result = _repository.GetAll().Where(x => x.Status == (int)Helpers.status.Active );

            var recipeviewmodel = _mapper.Map<List<RecipeViewModel>>(data);

            return View(recipeviewmodel);

        }
      
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                var dto = _mapper.Map<UserDTO>(user);
                var check = _userRepository.GetAll().Where(u => u.Email == user.Email).FirstOrDefault();

                if (check == null)
                {

                    _userRepository.Create(dto,0);
                    
                    return RedirectToAction("Login");
                }

                else
                {
                    Session["RegisterError"] = true;
                    return RedirectToAction("Registration");

                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["email"] = null;
            Session["userId"] = null;
            Session["username"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
           
                var check = _recipeContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();
                if (check != null)
                {


                    if (Crypto.VerifyHashedPassword(check.Password, user.Password))
                    {
                        Session["email"] = check.Email;
                        Session["username"] = check.Name;
                        Session["userId"] = check.Id;
                        //if (Session["username"].ToString() == "admin")
                        //{
                        //    return View("~/Areas/Manage/Views/Home/Index.cshtml");
                        //}
                        //else
                        //{
                        return RedirectToAction("Home", "Recipes");
                        //}

                    }
                    else
                    {
                        TempData["PasswordError"] = "Invalid password";
                    }

                
               
            
            }
            return View();
        }

        [HttpGet]
        public ActionResult RecipeDetails()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RecipeGrid()
        {
            return View();
        }

    }
}