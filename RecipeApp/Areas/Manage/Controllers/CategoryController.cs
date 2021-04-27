using AutoMapper;
using RecipeApp.Models;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Category;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Areas.Manage.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;


        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        // GET: Manage/Category
        public ActionResult Index()
        {
           var data= _repository.GetAll();

            var catdtos = _mapper.Map<List<CategoryViewModel>>(data);

            return View(catdtos);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {

            model.UserId = 2;
            var persondtos = _mapper.Map<RecipeBLL.DTOS.CategoryDTO>(model);
            //var categoryModel = _mapper.Map<Category>(persondtos);
            _repository.Create(persondtos, 2
                );

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = _repository.GetById(id);
      
            var viewModel = _mapper.Map<CategoryViewModel>(model);


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {

            model.UserId = 2;
            var dtos = _mapper.Map<RecipeBLL.DTOS.CategoryDTO>(model);
            var categoryModel = _mapper.Map<Category>(dtos);
            _repository.Update(dtos, 2);

            return View(model);
        }

        public ActionResult Details (int id)
        {
            var data = _repository.GetById(id);
            var dtos = _mapper.Map<CategoryViewModel>(data);
            return View(dtos);
        }
        public ActionResult Delete(int id)
        {
            _repository.Delete(id,2);
            
            return RedirectToAction("Index");
        }
    }
}