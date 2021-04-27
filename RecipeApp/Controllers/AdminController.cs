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
       
    }
}