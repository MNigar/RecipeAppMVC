using AutoMapper;
using RecipeApp.Models;
using RecipeApp.Models.ViewModels;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Category;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RecipeApp.Areas.Manage.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly RecipeContext _context;

        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository, IMapper mapper, RecipeContext context)
        {
            _mapper = mapper;
            _repository = repository;
            _context = context;
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
        public ActionResult Create(CategoryViewModel model, HttpPostedFileBase Photo)
        {
            string fName = Photo.FileName;
            string path = Path.Combine(Server.MapPath("~/Upload"), fName);
            Photo.SaveAs(path);
            model.UserId = (int)Session["userId"];
            model.Photo = fName;
            var persondtos = _mapper.Map<RecipeBLL.DTOS.CategoryDTO>(model);
            //var categoryModel = _mapper.Map<Category>(persondtos);
            _repository.Create(persondtos, 2
                );
           
              


            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var model = _repository.GetById(id);
      
            var viewModel = _mapper.Map<CategoryViewModel>(model);


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model, HttpPostedFileBase Photo)
        {
            Category current = _context.Categories.Find(model.Id);

            if (Photo != null)
            {
                string fName = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Upload"), fName);
                Photo.SaveAs(path);
                model.Photo = fName;
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Upload"), current.Photo));
                _context.Entry(current).State = EntityState.Detached;
            }
            model.UserId = (int)Session["userId"];
            if (Photo == null)
            {
                var dto = _mapper.Map<CategoryDTO>(model);
                var contextModel = _mapper.Map<Category>(dto);
                //_context.Entry(contextModel).Property(p => p.Photo).IsModified = false;
                model.Photo = current.Photo;
            }
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