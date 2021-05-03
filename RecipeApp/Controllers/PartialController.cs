using AutoMapper;
using RecipeApp.Models;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Logo;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Views.Recipes
{
    public class PartialController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _repository;
        private readonly ICategoryRepository _catrepository;
        private readonly IIngridientRepository _ingridientrepository;
        private readonly RecipeContext _recipeContext;
        private readonly ILogoRepository _logoRepository;
        public PartialController(IRecipeRepository repository, IMapper mapper, ICategoryRepository catrepository,
            IIngridientRepository ingridientrepository, RecipeContext recipeContext, ILogoRepository logoRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _catrepository = catrepository;
            _ingridientrepository = ingridientrepository;
            _recipeContext = recipeContext;
            _logoRepository = logoRepository;
        }
        // GET: Partial
        public ActionResult CategoryList()
        {
            var data = _catrepository.GetAll();
            //var categoryImage = data.Take(2);
            //var categoryImageView = _mapper.Map<List<CategoryViewModel>>(categoryImage);
            var result = _mapper.Map<List<CategoryViewModel>>(data);
            return PartialView(result);
        }
        public ActionResult Logo()
        {
            var logo = _logoRepository.GetAll().LastOrDefault();
            var logoView = _mapper.Map<LogoViewModel>(logo);
            return PartialView(logoView);
        }
    }
}