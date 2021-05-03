using RecipeApp.Helper;
using RecipeApp.Models;
using RecipeApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeApp.Utils.Recipe
{
    public class RecipeData: Controller
    {
        public Tuple<List<string>,List<string>> IngArray(FormCollection form)
        {
            List<string> arrList = new List<string>();
            List<string> arrListQuantity = new List<string>();


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
            return Tuple.Create(arrList, arrListQuantity);
        }
        public void Creates(FormCollection form, HttpPostedFileBase Photo)
        {

            RecipeIngridientViewModel model = new RecipeIngridientViewModel();

            model.RecipeViewModel = new RecipeViewModel();
            List<IngridientViewModel> newlist = new List<IngridientViewModel>();

            string fName = Photo.FileName;
            string path = Path.Combine(Server.MapPath("~/Upload"), fName);
            Photo.SaveAs(path);
            model.RecipeViewModel.Status = (int)Helpers.status.Waiting;
            model.RecipeViewModel.UserId = (int)Session["userId"];
            model.RecipeViewModel.EditedId = 0;
            model.RecipeViewModel.Name =Request.Form["name"];
            model.RecipeViewModel.Description = Request.Form["Description"];
            model.RecipeViewModel.CategoryId = Convert.ToInt32(Request.Form["categoryId"]);
            model.RecipeViewModel.Photo = fName;
            model.RecipeViewModel.Duration = Request.Form["Duration"];
            model.IngridientViewModel = new IngridientList();
            model.IngridientViewModel.IngridientLists = new List<IngridientViewModel>();
            var ingridinetlist = new List<string>();
            var result = IngArray(form);
            foreach (var i in result.Item1)
            {
                newlist.Add(new IngridientViewModel()
                {
                    Name = i,
                    Quantity = result.Item2[result.Item1.IndexOf(i)],
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

           
        }
    }
}