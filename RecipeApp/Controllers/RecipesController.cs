using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RecipeApp.Helper;
using RecipeApp.Models;
using RecipeApp.Models.ViewModels;
using RecipeApp.Utils.Recipe;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Logo;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using Unity;

namespace RecipeApp.Controllers
{
    
    public class RecipesController : Controller
    {
        RecipeData method = new RecipeData();
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        private readonly RecipeContext _recipeContext;
        private readonly ILogoRepository _logoRepository;
        public RecipesController(IRecipeRepository repository ,IMapper mapper, ICategoryRepository catrepository,
            IIngridientRepository ingridientrepository, RecipeContext recipeContext, ILogoRepository logoRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
            _logoRepository = logoRepository;
        }
        // GET: Recipes
        [HttpGet]
      public ActionResult  Index()
        {
            var data = _repository.GetAll();
            var recipeviewmodel = _mapper.Map<List<RecipeViewModel>>(data);
            return View(recipeviewmodel);
        }
       

        [HttpGet]
        public ActionResult SubmitRecipe()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RecipeDetails(int id)
        {

            var data = _repository.GetById(id);
            var result = _mapper.Map<RecipeViewModel>(data);
            
            ViewBag.image = result.Photo;
            ViewBag.day = data.CreatedDate.Day;
            ViewBag.Month = data.CreatedDate.ToString("MMMM");           
            ViewBag.Date = method.RecipeDate(DateTime.Now, data.CreatedDate);
            var dataList = _repository.GetAll().Where(x=>x.Status==(int)Helpers.status.Active).Take(2);            
            var recipeList = _mapper.Map<List<RecipeViewModel>>(dataList);
            ViewBag.Recipes = recipeList;
            
            return View(result);
            
        }

        [HttpGet]
        public ActionResult RecipeGrid()
        {
            var data = _repository.GetAll().Where(x=>x.Status==(int) Helpers.status.Active);
            var recipeViewModel = _mapper.Map<List<RecipeViewModel>>(data);
            //ViewBag.Category = recipeViewModel.Select(x => x.Category).ToList().Distinct();
            ViewBag.Category = _catrepository.GetAll().Distinct();
            return View(recipeViewModel);
        }

        [HttpGet]
        public ActionResult Search(string Name, string categoryId,string user)
        {
            var result = _repository.GetAll().Where(x=>x.Status==(int)Helpers.status.Active);
            
            if (!String.IsNullOrEmpty(Name))
            {
                result = result.Where(x => x.Name.ToLower().Contains(Name.ToLower()));
            }

            if (!String.IsNullOrEmpty(user))
            {
                result = result.Where(x => x.User.Name.ToLower().Contains(user.ToLower()));
            }
            if (!String.IsNullOrEmpty(categoryId))
            {
                result = result.Where(x => x.CategoryId==Convert.ToInt32(categoryId));
            }
            var recipeViewModel = _mapper.Map<List<RecipeViewModel>>(result);
            ViewBag.Category = _catrepository.GetAll().Distinct();
            return View(recipeViewModel);
           
        }


        public ActionResult Creates()
        {
            if (Session["username"] == null)
            {
                TempData["Message"] = "Please login before submit recipe";
                return RedirectToAction("Login", "Home");
            }
            var data = _catrepository.GetAll();
            var catViewModel = _mapper.Map<List<CategoryViewModel>>(data);
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();
            model.RecipeViewModel = new RecipeViewModel();
            model.IngridientViewModel = new IngridientList();
            ViewBag.Category = catViewModel;
           
            return View(model);
        }
       
        [HttpPost]
        public ActionResult Creates(FormCollection form, HttpPostedFileBase Photo)
        {                                    
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();
            
            var createdRecipe = method.Creates(form, Photo);
            model.RecipeViewModel = createdRecipe;

            string fName = Photo.FileName;
            string path = Path.Combine(Server.MapPath("~/Upload"), fName);
            Photo.SaveAs(path);

            model.RecipeViewModel.Photo = fName;
            model.RecipeViewModel.UserId = (int)Session["userId"];
           
            var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
            _repository.Create(recipe, model.RecipeViewModel.UserId);
            
            var id = _recipeContext.Recipes.ToList().LastOrDefault();
            var ingResult = RecipeData.CreateIngridient(form, id.Id);
           
            model.IngridientViewModel = new IngridientList();
            model.IngridientViewModel.IngridientLists = ingResult;
          
            foreach(var i in model.IngridientViewModel.IngridientLists)
            {
                var ingridient = _mapper.Map<IngridientDTO>(i);
                _ingridientrepository.Create(ingridient, 0);

            }
            Email.SendEmail(Session["email"].ToString(), Session["username"].ToString(), "reseptiniz admin terefinden qiymetlendirilecek", model.RecipeViewModel.Name);
          
            return RedirectToAction("RecipeGrid");
        }

        public ActionResult Edit(int id)
        {
            var model = _repository.GetById(id);
            var recipe = _mapper.Map<RecipeViewModel>(model);
            var data = _catrepository.GetAll();
            var catViewModel = _mapper.Map<List<CategoryViewModel>>(data);
            RecipeIngridientViewModel viewModel = new RecipeIngridientViewModel();
            viewModel.RecipeViewModel = recipe;   
            var category = _catrepository.GetAll();
            var catList = _mapper.Map<List<CategoryViewModel>>(category);
            ViewBag.Category = catList;
           
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(RecipeIngridientViewModel model, FormCollection form, HttpPostedFileBase Photo)
        {
            List<IngridientViewModel> newlist = new List<IngridientViewModel>();
            int mId = Convert.ToInt32(Request.Form["Id"]);         
            var createdRecipe = method.Creates(form, Photo);
            model.RecipeViewModel = createdRecipe;
            var findRecipe = _repository.GetById(mId);
            var currentRec = _mapper.Map<Recipe>(findRecipe);

            if (Photo != null)
            {

                string fName = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Upload"), fName);
                Photo.SaveAs(path);
                model.RecipeViewModel.Photo = fName;

                Recipe currentRecipeModel = currentRec;
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Upload"), currentRecipeModel.Photo));
                _recipeContext.Entry(currentRecipeModel).State = EntityState.Detached;

            }
        
            model.RecipeViewModel.UserId = (int)Session["userId"];          
            model.RecipeViewModel.EditedId = mId;
            model.RecipeViewModel.Status = (int)Helpers.status.Active;

            var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
            _repository.Create(recipe, model.RecipeViewModel.UserId);
            
            var id = _recipeContext.Recipes.ToList().LastOrDefault();
            var ingResult = RecipeData.CreateIngridient(form, id.Id);

            model.IngridientViewModel = new IngridientList();
            model.IngridientViewModel.IngridientLists = ingResult;
            
            foreach (var i in model.IngridientViewModel.IngridientLists)
            {
                var ingridient = _mapper.Map<IngridientDTO>(i);
                _ingridientrepository.Create(ingridient, 0);
            }
            Email.SendEmail(Session["email"].ToString(), Session["username"].ToString(), "reseptiniz admin terefinden qiymetlendirirlecek", model.RecipeViewModel.Name);

            return RedirectToAction("Profiles","Home");
        }

        public ActionResult Details(int id)
        {
            var data = _repository.GetById(id);
            var dtos = _mapper.Map<RecipeViewModel>(data);

            return View(dtos);
        }

        public ActionResult Delete(int id)
        {
            var result=_repository.GetById(id);
            result.Status = (int)Helper.Helpers.status.Deactive;
            _repository.Update(result, Convert.ToInt32(Session["userId"]));

            return RedirectToAction("Profiles","Home");
        }

        public ActionResult Home()
        {
            var data = _catrepository.GetAll();
            var category = _mapper.Map<List<CategoryViewModel>>(data);
                      
            return View(category);
        }
       


    }
}
