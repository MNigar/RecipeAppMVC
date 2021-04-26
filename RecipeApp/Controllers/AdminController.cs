using AutoMapper;
using RecipeApp.Models;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;


        private readonly ICategoryRepository _repository;

        public AdminController(ICategoryRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
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
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(CategoryViewModel model)
        {
           
            model.UserId = 2;
            var persondtos = _mapper.Map<RecipeBLL.DTOS.CategoryDTO>(model);
            var categoryModel = _mapper.Map<Category>(persondtos);
            _repository.Create(categoryModel,2
                ) ;

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = _repository.GetById(id);
            var dtos = _mapper.Map<RecipeBLL.DTOS.CategoryDTO>(model);
            var viewModel = _mapper.Map<CategoryViewModel>(dtos);


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {

            model.UserId = 2;
            var dtos = _mapper.Map<RecipeBLL.DTOS.CategoryDTO>(model);
            var categoryModel = _mapper.Map<Category>(dtos);
            _repository.Update(categoryModel, 2);

            return View(model);
        }
    }
}