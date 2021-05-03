using AutoMapper;
using RecipeApp.Controllers;
using RecipeApp.Models;
using RecipeApp.Models.ViewModels;
using RecipeBLL.DTOS;
using RecipeBLL.Repository.Category;
using RecipeBLL.Repository.Ingridient;
using RecipeBLL.Repository.Logo;
using RecipeBLL.Repository.Recipe;
using RecipeBLL.Repository.User;
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
            container.RegisterType<IIngridientRepository, RecipeBLL.Repository.Ingridient.IngridientRepository>();

            container.RegisterType<ICategoryRepository, RecipeBLL.Repository.Category.CategoryRepository>();
            container.RegisterType<IUserRepository, RecipeBLL.Repository.User.UserRepository>();
            container.RegisterType<ILogoRepository, LogoRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                //Create all maps here
                cfg.CreateMap<RecipeViewModel, RecipeDTO>();
                cfg.CreateMap<RecipeDTO, RecipeViewModel>();
                cfg.CreateMap<RecipeDTO, Recipe>();
                cfg.CreateMap<Recipe, RecipeDTO>();
                cfg.CreateMap<CategoryViewModel, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO,CategoryViewModel>();
                cfg.CreateMap<IngridientViewModel, IngridientDTO>();
                cfg.CreateMap<IngridientDTO, Ingridient>();
                cfg.CreateMap< IngridientList,IngridinetListDTO>();
                cfg.CreateMap<Ingridient, IngridientDTO>();
                cfg.CreateMap<UserViewModel, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, UserViewModel>();
                cfg.CreateMap<LogoViewModel, LogoDTO>();
                cfg.CreateMap<LogoDTO, Logo>();
                cfg.CreateMap<Logo, LogoDTO>();
                cfg.CreateMap<LogoDTO, LogoViewModel>();
                
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