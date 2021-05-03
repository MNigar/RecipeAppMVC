using AutoMapper;
using RecipeApp.Models;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Logo;
using RecipeDAL.Context;
using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Areas.Manage.Controllers
{
    public class LogoController : Controller
    {
        private readonly RecipeContext _recipeContext;
        private readonly IMapper _mapper;
        private readonly ILogoRepository _repository;
        public LogoController(ILogoRepository repository, IMapper mapper,  RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
            _mapper = mapper;
            _repository = repository;
        }
            // GET: Manage/Logo
            public ActionResult Index()
        {
            var modelById = _repository.GetAll();
            var result = _mapper.Map<List<LogoViewModel>>(modelById);
            return View(result);
        }
       public ActionResult Create()
        {
            LogoViewModel model = new LogoViewModel();
            return View();
        }
        [HttpPost]
        public ActionResult Create(LogoViewModel model, HttpPostedFileBase Photo)
        {
            string fName = Photo.FileName;
            string path = Path.Combine(Server.MapPath("~/Upload"), fName);
            Photo.SaveAs(path);
            model.Photo = fName;
            var dto = _mapper.Map<LogoDTO>(model);
            _repository.Create(dto, 0);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var modelById = _repository.GetById(id);
            var result = _mapper.Map<LogoViewModel>(modelById);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(LogoViewModel model, HttpPostedFileBase Photo)
        {
            Logo current = _recipeContext.Logos.Find(model.Id);

            if (Photo != null)
            {
                string fName = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Upload"), fName);
                Photo.SaveAs(path);
                model.Photo = fName;
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Upload"), current.Photo));
                _recipeContext.Entry(current).State = EntityState.Detached;
            }
            var dto = _mapper.Map<LogoDTO>(model);
            _repository.Update(dto, 0);
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var modelById = _repository.GetById(id);
            var result = _mapper.Map<LogoViewModel>(modelById);
            return View(result);
        }
      
        public ActionResult Delete(int id)
        {
          
             _repository.Delete(id,0);

            return RedirectToAction("Index");

        }

    }
}