namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedphotocolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Photo");
        }
    }
}
