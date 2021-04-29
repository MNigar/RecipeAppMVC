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
using RecipeDAL.Context;
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
        private readonly RecipeContext _recipeContext;
        public RecipesController(IRecipeRepository repository ,IMapper mapper, ICategoryRepository catrepository, IIngridientRepository ingridientrepository, RecipeContext recipeContext)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
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


      

        public ActionResult Creates()
        {
            var data = _catrepository.GetAll();

            var catViewModel = _mapper.Map<List<CategoryViewModel>>(data);
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();
            model.RecipeViewModel = new RecipeViewModel();
            model.IngridientViewModel = new IngridientList();
            
            ViewBag.Category = catViewModel;


            return View(model);
        }
        //[HttpPost]
        //public ActionResult Creates(RecipeIngridientViewModel model)
        //{


        //    ////model.RecipeViewModel = new RecipeViewModel();
        //    //model.RecipeViewModel.Status = 0;
        //    //model.RecipeViewModel.UserId = 2;
        //    var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
        //    var ingridients = _mapper.Map<IngridientDTO>(model.IngridientViewModel);
            
        //    //var categoryModel = _mapper.Map<Category>(persondtos);
        //    _repository.Create(recipe, 2);
        //    _ingridientrepository.Create(ingridients, 0);
        //    return View(model);
        //}
        [HttpPost]
        public void Creates(FormCollection form)
        {
            var forms = new Dictionary<string, string>();
            List<IngridientViewModel> newlist = new List<IngridientViewModel>();
            List<string> arrList = new List<string>();
            List<string> arrListQuantity = new List<string>();

        
            foreach (var key in form.AllKeys)
            {
                var value = form[key];
                if (key == "IngridientName")
                {
                  string[]  arr = value.Split(',');
                    foreach(var i in arr)
                    {
                        arrList.Add(i);
                    }
                    
                }
                if (key == "IngridientQuantity")
                {
                    string[] arr1 = value.Split(',');
                    foreach (var i in arr1)
                    {
                        arrListQuantity.Add(i);
                    }
                }
            }
           
        

            var id = _recipeContext.Recipes.OrderByDescending(x => x.Id).FirstOrDefault();
            var t = (id != null) ? id.Id++ : 1;
         
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();
            model.RecipeViewModel = new RecipeViewModel();
            model.RecipeViewModel.Status = 0;
            model.RecipeViewModel.UserId = 2;
            model.RecipeViewModel.Id = t;
            model.RecipeViewModel.Name = Request.Form["name"];
            model.RecipeViewModel.Description = Request.Form["Description"];
            model.RecipeViewModel.CategoryId =Convert.ToInt32 (Request.Form["categoryId"]);
            var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
            _repository.Create(recipe, model.RecipeViewModel.UserId);
            model.IngridientViewModel = new IngridientList();
            model.IngridientViewModel.IngridientLists = new List<IngridientViewModel>();
            var ingridinetlist = new List<string>();
            var list = new List<Tuple<string, string>>();
            foreach (var i in arrList)
            {
                newlist.Add(new IngridientViewModel()
                {
                    Name = i,
                    Quantity = arrListQuantity[arrList.IndexOf(i)],
                    RecipeId = model.RecipeViewModel.Id
                });
            }

            Request.Form["name"].Split(',');
       
                model.IngridientViewModel.IngridientLists.Add(new IngridientViewModel());

            //new IngridientViewModel() { 
            //Name = Request.Form["IngridientName"] ,
            //Quantity=Request.Form["IngridientQuantity"],
            //RecipeId= model.RecipeViewModel.Id});
           
            var ingridients = _mapper.Map<IngridinetListDTO>(model.IngridientViewModel);
            foreach(var i in ingridients.IngridientList)
            {
                _ingridientrepository.Create(i, 0);

            }

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
