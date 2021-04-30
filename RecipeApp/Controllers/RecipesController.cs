using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RecipeApp.Helper;
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
        public ActionResult SubmitRecipe()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RecipeDetails(int id)
        {


            var data = _repository.GetById(id);

            var result = _mapper.Map<RecipeViewModel>(data);
            ViewBag.image = result.Photo;
            ViewBag.day = data.CreatedDate.Day;
            ViewBag.Month = data.CreatedDate.ToString("MMMM");

            var dataList = _repository.GetAll().Where(x=>x.Status==(int)Helpers.status.Active).Take(2);
            
            var recipeList = _mapper.Map<List<RecipeViewModel>>(dataList);
            ViewBag.Recipes = recipeList;
            
            return View(result);
            
        }
        [HttpGet]
        public ActionResult RecipeGrid()
        {
            var data = _repository.GetAll().Where(x=>x.Status==(int) Helpers.status.Active);
            var recipeViewModel = _mapper.Map<List<RecipeViewModel>>(data);
            //ViewBag.Category = recipeViewModel.Select(x => x.Category).ToList().Distinct();
            ViewBag.Category = _catrepository.GetAll().Distinct();
            return View(recipeViewModel);
        }
        [HttpGet]
        public ActionResult Search(string Name, string categoryId,string user)
        {
            var result = _repository.GetAll().Where(x=>x.Status==(int)Helpers.status.Active);
            
            if (!String.IsNullOrEmpty(Name))
            {
                result = result.Where(x => x.Name.ToLower().Contains(Name.ToLower()));
            }

            if (!String.IsNullOrEmpty(user))
            {
                result = result.Where(x => x.User.Name.ToLower().Contains(user.ToLower()));
            }
            if (!String.IsNullOrEmpty(categoryId))
            {
                result = result.Where(x => x.CategoryId==Convert.ToInt32(categoryId));
            }
            var recipeViewModel = _mapper.Map<List<RecipeViewModel>>(result);
            ViewBag.Category = _catrepository.GetAll().Distinct();
            return View(recipeViewModel);
           

        }



        public ActionResult Creates()
        {
            if (Session["username"] == null)
            {
                TempData["Message"] = "Please login before submit recipe";
                return RedirectToAction("Login", "Home");
            }
            var data = _catrepository.GetAll();

            var catViewModel = _mapper.Map<List<CategoryViewModel>>(data);
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();
            model.RecipeViewModel = new RecipeViewModel();
            model.IngridientViewModel = new IngridientList();
            
            ViewBag.Category = catViewModel;


            return View(model);
        }
       
        [HttpPost]
        public ActionResult Creates(FormCollection form, HttpPostedFileBase Photo)
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
            
            var t = (id != null) ? id.Id+1 : 1;
         
            RecipeIngridientViewModel model = new RecipeIngridientViewModel();

            model.RecipeViewModel = new RecipeViewModel();
            
            string fName = Photo.FileName;
            string path = Path.Combine(Server.MapPath("~/Upload"), fName);
            Photo.SaveAs(path);
            model.RecipeViewModel.Status =(int) Helpers.status.Waiting;
            model.RecipeViewModel.UserId = (int)Session["userId"];
            model.RecipeViewModel.Id = t;
            model.RecipeViewModel.EditedId = 0;
            model.RecipeViewModel.Name = Request.Form["name"];
            model.RecipeViewModel.Description = Request.Form["Description"];
            model.RecipeViewModel.CategoryId =Convert.ToInt32 (Request.Form["categoryId"]);
            model.RecipeViewModel.Photo = fName;
            model.RecipeViewModel.Duration= Request.Form["Duration"];
            var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
            _repository.Create(recipe, model.RecipeViewModel.UserId);
            model.IngridientViewModel = new IngridientList();
            model.IngridientViewModel.IngridientLists = new List<IngridientViewModel>();
            var ingridinetlist = new List<string>();
            foreach (var i in arrList)
            {
                newlist.Add(new IngridientViewModel()
                {
                    Name = i,
                    Quantity = arrListQuantity[arrList.IndexOf(i)],
                    RecipeId = model.RecipeViewModel.Id
                });
            }

           
            foreach(var ing in newlist)
            {
                model.IngridientViewModel.IngridientLists.Add(new IngridientViewModel()
                {   Name=ing.Name,
                    Quantity=ing.Quantity,
                    RecipeId=ing.RecipeId
                });

            }

            foreach(var i in model.IngridientViewModel.IngridientLists)
            {
                var ingridient = _mapper.Map<IngridientDTO>(i);
                _ingridientrepository.Create(ingridient, 0);

            }
            int port = 587;
            string smtpServer = "smtp.gmail.com";
            string smtpUserName = "tricklyrecipeapp@gmail.com";
            string smtpUserPass = "bilmirem@";

            using (SmtpClient smtpSend = new SmtpClient())
            {
                smtpSend.Host = smtpServer;
                smtpSend.Port = port;

                smtpSend.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpUserPass);

                smtpSend.EnableSsl = true;

                MailMessage emailMessage = new System.Net.Mail.MailMessage();

                emailMessage.To.Add(Session["email"].ToString());
                emailMessage.From = new MailAddress("tricklyrecipeapp@gmail.com");
                emailMessage.Subject = "Hormetli"+" "+Session["username"]+model.RecipeViewModel.Name+"reseptiniz admin terefinden qiymetlendirirlecek";
                emailMessage.Body = "Hormetli" + " " + Session["username"] + model.RecipeViewModel.Name + "reseptiniz admin terefinden qiymetlendirirlecek";



                smtpSend.Send(emailMessage);
            }

            return RedirectToAction("RecipeGrid");
        }
        public ActionResult Edit(int id)
        {
            var model = _repository.GetById(id);

            var recipe = _mapper.Map<RecipeViewModel>(model);
            var data = _catrepository.GetAll();

            var catViewModel = _mapper.Map<List<CategoryViewModel>>(data);
            RecipeIngridientViewModel viewModel = new RecipeIngridientViewModel();
            viewModel.RecipeViewModel = recipe;
            //viewModel.IngridientViewModel = new IngridientList();
            var category = _catrepository.GetAll();

            var catList = _mapper.Map<List<CategoryViewModel>>(category);
            ViewBag.Category = catList;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(RecipeIngridientViewModel model, FormCollection form, HttpPostedFileBase Photo)
        {
            List<IngridientViewModel> newlist = new List<IngridientViewModel>();
            List<string> arrList = new List<string>();
            List<string> arrListQuantity = new List<string>();

            //RecipeIngridientViewModel model = new RecipeIngridientViewModel();

            //model.RecipeViewModel = new RecipeViewModel();
            model.RecipeViewModel = new RecipeViewModel();
            int mId = Convert.ToInt32(Request.Form["Id"]);
            foreach (var key in form.AllKeys)
            {
                var value = form[key];
                if (key == "IngridientName")
                {
                    string[] arr = value.Split(',');
                    foreach (var i in arr)
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
            var findRecipe = _repository.GetById(mId);
            var currentRec = _mapper.Map<Recipe>(findRecipe);
            var photorecipe = _mapper.Map<RecipeViewModel>(findRecipe);
            if (Photo != null)
            {

                string fName = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Upload"), fName);
                Photo.SaveAs(path);
                model.RecipeViewModel.Photo = fName;


                Recipe currentRecipeModel = currentRec;
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Upload"), currentRecipeModel.Photo));
                _recipeContext.Entry(currentRecipeModel).State = EntityState.Detached;

            }

            var id = _recipeContext.Recipes.OrderByDescending(x => x.Id).FirstOrDefault();
            var newid = id.Id + 1;
            var t = (id != null) ? newid : 1;
            model.RecipeViewModel.Id = t;
            model.RecipeViewModel.Status = 1;
            model.RecipeViewModel.UserId = 2;
            model.RecipeViewModel.Name = Request.Form["name"];
            model.RecipeViewModel.Description = Request.Form["Description"];
            model.RecipeViewModel.CategoryId = Convert.ToInt32(Request.Form["categoryId"]);
            model.RecipeViewModel.Photo = photorecipe.Photo;
            model.RecipeViewModel.Duration = Request.Form["Duration"];
            model.RecipeViewModel.EditedId = mId;
            var recipe = _mapper.Map<RecipeBLL.DTOS.RecipeDTO>(model.RecipeViewModel);
            _repository.Create(recipe, model.RecipeViewModel.UserId);
            model.IngridientViewModel = new IngridientList();
            model.IngridientViewModel.IngridientLists = new List<IngridientViewModel>();
            var ingridinetlist = new List<string>();
            foreach (var i in arrList)
            {
                newlist.Add(new IngridientViewModel()
                {
                    Name = i,
                    Quantity = arrListQuantity[arrList.IndexOf(i)],
                    RecipeId = model.RecipeViewModel.Id
                });
            }

            foreach (var ing in newlist)
            {
                model.IngridientViewModel.IngridientLists.Add(new IngridientViewModel()
                {
                    Name = ing.Name,
                    Quantity = ing.Quantity,
                    RecipeId = ing.RecipeId
                });

            }


            var ingList = _recipeContext.Recipes.FirstOrDefault(x => x.Id == model.RecipeViewModel.Id).Ingridients;


            //sonra lazim olacaq
            //foreach(var ingid in ingList)
            //{
            //    var ingmap = _mapper.Map<IngridientDTO>(ingid);
            //    _ingridientrepository.Delete(ingmap.Id,model.RecipeViewModel.UserId);

            //}


            foreach (var i in model.IngridientViewModel.IngridientLists)
            {
                var ingridient = _mapper.Map<IngridientDTO>(i);

                _ingridientrepository.Create(ingridient, 0);

            }
            int port = 587;
            string smtpServer = "smtp.gmail.com";
            string smtpUserName = "tricklyrecipeapp@gmail.com";
            string smtpUserPass = "bilmirem@";

            using (SmtpClient smtpSend = new SmtpClient())
            {
                smtpSend.Host = smtpServer;
                smtpSend.Port = port;

                smtpSend.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpUserPass);

                smtpSend.EnableSsl = true;

                MailMessage emailMessage = new System.Net.Mail.MailMessage();

                emailMessage.To.Add(Session["email"].ToString());
                emailMessage.From = new MailAddress("tricklyrecipeapp@gmail.com");
                emailMessage.Subject = "Hormetli" + " " + Session["username"] + model.RecipeViewModel.Name + "reseptiniz admin terefinden qiymetlendirirlecek";
                emailMessage.Body = "Hormetli" + " " + Session["username"] + model.RecipeViewModel.Name + "reseptiniz admin terefinden qiymetlendirirlecek";



                smtpSend.Send(emailMessage);
            }


            return RedirectToAction("Profiles");
        }
        public ActionResult Details(int id)
        {
            var data = _repository.GetById(id);
            var dtos = _mapper.Map<RecipeViewModel>(data);
            return View(dtos);
        }
        public ActionResult Delete(int id)
        {
            var result=_repository.GetById(id);
            result.Status = (int)Helper.Helpers.status.Deactive;
            _repository.Update(result, Convert.ToInt32(Session["userId"]));

            return RedirectToAction("Profile");
        }
        public ActionResult Home()
        {
            var data = _catrepository.GetAll();
            var category = _mapper.Map<List<CategoryViewModel>>(data);

            return View(category);
        }


    }
}
