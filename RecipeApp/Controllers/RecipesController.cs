using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RecipeApp.Models;
using RecipeApp.Models.ViewModels;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.DAL;
using Unity;

namespace RecipeApp.Controllers
{
    
    public class RecipesController : Controller
    {
        private readonly IMapper _mapper;


        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        public RecipesController(IRecipeRepository repository ,IMapper mapper, ICategoryRepository catrepository, IIngridientRepository ingridientrepository)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
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
     public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SubmitRecipe()
        {
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


      

        public ActionResult Create()
        {
            var data = _catrepository.GetAll();

            var catViewModel = _mapper.Map<List<CategoryViewModel>>(data);
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();
            model.RecipeViewModel = new RecipeViewModel();
            model.IngridientViewModel = new IngridientViewModel();
            
            ViewBag.Category = catViewModel;


            return View(model);
        }
        [HttpPost]
        public ActionResult Create(RecipeIngridientViewModel model)
        {

            
            
            var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
            var ingridients = _mapper.Map<IngridientDTO>(model.IngridientViewModel);

            //var categoryModel = _mapper.Map<Category>(persondtos);
            _repository.Create(recipe, 2
                );
            _ingridientrepository.Create(ingridients, 2);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = _repository.GetById(id);

            var viewModel = _mapper.Map<RecipeViewModel>(model);


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(RecipeViewModel model)
        {

            model.UserId = 2;
            var dtos = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model);
            var categoryModel = _mapper.Map<Recipe>(dtos);
            _repository.Update(dtos, 2);

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var data = _repository.GetById(id);
            var dtos = _mapper.Map<RecipeViewModel>(data);
            return View(dtos);
        }
        public ActionResult Delete(int id)
        {
            _repository.Delete(id, 2);

            return RedirectToAction("Index");
        }


    }
}
