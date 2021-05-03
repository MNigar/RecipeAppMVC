using RecipeDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDAL.Context
{
   public class RecipeContext:DbContext
    {
        public RecipeContext() : base("RecipeApp") { }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Ingridient> Ingridients { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Logo> Logos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}
