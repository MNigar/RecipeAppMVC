namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Photo", c => c.String());
            AddColumn("dbo.Recipes", "EditedId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "EditedId");
            DropColumn("dbo.Categories", "Photo");
        }
    }
}
