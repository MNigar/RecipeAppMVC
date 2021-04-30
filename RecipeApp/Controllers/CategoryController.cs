using AutoMapper;
using RecipeApp.Helper;
using RecipeApp.Models;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;


        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        private readonly RecipeContext _recipeContext;
        public CategoryController(IRecipeRepository repository, IMapper mapper, ICategoryRepository catrepository, IIngridientRepository ingridientrepository, RecipeContext recipeContext)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult RecipeCategory()
        {
            var data = _catrepository.GetAll();
            var category = _mapper.Map<List<CategoryViewModel>>(data);
           
            return View(category);
        }
        public ActionResult RecipeGrid(int id)
        {
            var data = _repository.GetAll().Where(x => x.Status == (int)Helpers.status.Active && x.CategoryId==id);
            var recipeViewModel = _mapper.Map<List<RecipeViewModel>>(data);
            ViewBag.Category = recipeViewModel.Select(x => x.Category).ToList().Distinct();
            return View(recipeViewModel);
        }
    }
}