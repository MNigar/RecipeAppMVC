using AutoMapper;
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

namespace RecipeApp.Areas.Manage.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        private readonly RecipeContext _recipeContext;
        private readonly IMapper _mapper;
        public RecipesController(IRecipeRepository repository, IMapper mapper, ICategoryRepository catrepository, IIngridientRepository ingridientrepository, RecipeContext recipeContext)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
        }
        // GET: Manage/Recipe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WaitingForSubmit()
        {
            var data = _repository.GetAll().Where(x=>x.Status==(int)Helper.Helpers.status.Non);
            var result = _mapper.Map<List<RecipeViewModel>>(data);
            return View(result);
        }
        [HttpPost]
        public ActionResult Submit(int id)
        {
            var data = _repository.GetById(id);
            var result = _mapper.Map<RecipeViewModel>(data);
            data.Status = 0;
            _repository.Update(data,2);

            return RedirectToAction("WaitingForSubmit");
        }
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var data = _repository.GetById(id);
            var result = _mapper.Map<RecipeViewModel>(data);
            data.Status = 3;
            _repository.Update(data, 2);

            return RedirectToAction("WaitingForSubmit");
        }
    }
}