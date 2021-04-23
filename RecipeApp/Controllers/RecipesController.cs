using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

using RecipeBLL.Repository.Recipe;
using Unity;

namespace RecipeApp.Controllers
{
    
    public class RecipesController : Controller
    {
        private readonly IMapper _mapper;


        private readonly IRecipeRepository _repository;

        public RecipesController(IRecipeRepository repository ,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        // GET: Recipes
        [HttpGet]
      public ActionResult  Index()
        {
            var persons =  _repository.GetAll().ToList();

            var persondtos = _mapper.Map<List<RecipeBLL.DTOS.RecipeDTO>>(persons);

            return View(persondtos);
        }
    }
}
