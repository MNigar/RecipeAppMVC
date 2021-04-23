using AutoMapper;
using RecipeApp.Controllers;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Recipe;
using RecipeDAL.DAL;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
namespace RecipeApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			//var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            var container = new UnityContainer();
            container.RegisterType<IRecipeRepository,RecipeBLL.Repository.Recipe.RecipeRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                //Create all maps here
                cfg.CreateMap<Recipes, RecipeDTO>();

               

                //...
            });
            IMapper mapper = config.CreateMapper();

            //var e = container.Resolve<RecipesController>();

            //e.Index();
            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));


        }
    }
}