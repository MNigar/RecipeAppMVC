using AutoMapper;
using RecipeApp.Helper;
using RecipeApp.Models;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Recipe;
using RecipeBLL.Repository.User;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Areas.Manage.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        private readonly IUserRepository _userRepository;
        private readonly RecipeContext _recipeContext;
        private readonly IMapper _mapper;
        public RecipesController(IRecipeRepository repository, IMapper mapper, ICategoryRepository catrepository, IIngridientRepository ingridientrepository, RecipeContext recipeContext, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
            _userRepository = userRepository;
        }
        // GET: Manage/Recipe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WaitingForSubmit()
        {
            var data = _repository.GetAll().Where(x=>x.Status==(int)Helper.Helpers.status.Non || x.Status==(int)Helper.Helpers.status.Waiting);
            var result = _mapper.Map<List<RecipeViewModel>>(data);
            return View(result);
        }






        //[HttpGet]
        //public ActionResult Submit(int id)
        //{
        //    return ()
        //}

        public ActionResult Details(int id)
        {
            var data=_repository.GetById(id);
            
            var result = _mapper.Map<RecipeViewModel>(data);
            ViewBag.image = result.Photo;
            return View(result);
        }
        //[HttpGet]
        //public ActionResult Submit(int id)
        //{
           
        //    var data = _repository.GetById(id);
        //    if (data == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(data);
        //}


    
        public ActionResult Submit(int id)
        {

            var data = _repository.GetById(id);
            var result = _mapper.Map<RecipeViewModel>(data);
            data.Status = (int)Helper.Helpers.status.Active;
            _repository.Update(data,2);
            
            var deleteddata = _repository.GetById(data.EditedId);
            if (deleteddata != null)
            {
                deleteddata.Status = (int)Helper.Helpers.status.Deactive;
                _repository.Update(deleteddata, 2);
            }
            Email.SendEmail(Session["email"].ToString(), Session["username"].ToString(), "reseptiniz qəbul edildi", result.Name);
            return RedirectToAction("WaitingForSubmit");
        }
     
        public ActionResult Cancel(int id)
        {
            var data = _repository.GetById(id);
            
            data.Status = (int)Helper.Helpers.status.Deactive;
            _repository.Update(data, 2);
            Email.SendEmail(Session["email"].ToString(), Session["username"].ToString(), "reseptiniz qəbul edilmədi", data.Name);
            return RedirectToAction("WaitingForSubmit");
        }
        public ActionResult TopUser()
        {
            var result= _userRepository.GetAll().OrderByDescending(x => x.Recipes.Count()).Take(5);
            var data = _mapper.Map<List<UserViewModel>>(result);
            return View(data);
        }
        
    }
}