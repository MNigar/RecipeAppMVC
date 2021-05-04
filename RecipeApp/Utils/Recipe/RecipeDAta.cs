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
        public static Tuple<List<string>,List<string>> IngArray(FormCollection form)
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
        public  RecipeViewModel Creates(FormCollection form, HttpPostedFileBase Photo)
        {

           RecipeViewModel model = new RecipeViewModel();


          
            model.Status = (int)Helpers.status.Waiting;
            model.EditedId = 0;
            model.Name = form["name"];

            model.Description = form["Description"];
            model.CategoryId = Convert.ToInt32(form["categoryId"]);
            model.Duration = form["Duration"];
           
           
            return model;
           
        }
        public static List<IngridientViewModel> CreateIngridient(FormCollection form,int id)
        {
            List<IngridientViewModel> newlist = new List<IngridientViewModel>();
            
            var ingridinetlist = new List<string>();
            var result = IngArray(form);
            foreach (var i in result.Item1)
            {
                newlist.Add(new IngridientViewModel()
                {
                    Name = i,
                    Quantity = result.Item2[result.Item1.IndexOf(i)],
                    RecipeId = id
                }) ;
            }
            return newlist;
          
            
        }
    }
}