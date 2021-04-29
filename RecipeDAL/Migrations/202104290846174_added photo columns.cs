namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedphotocolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Duration", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Duration");
        }
    }
}
