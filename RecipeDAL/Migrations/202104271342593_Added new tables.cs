namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        Name = c.String(),
                        Quantity = c.String(),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId)
                .Index(t => t.RecipeId);
            
            AddColumn("dbo.Recipes", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Recipes", "Description", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.Recipes", "Ingridients");
            DropColumn("dbo.Recipes", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "Quantity", c => c.String());
            AddColumn("dbo.Recipes", "Ingridients", c => c.String(maxLength: 200));
            DropForeignKey("dbo.Ingridients", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Ingridients", new[] { "RecipeId" });
            AlterColumn("dbo.Recipes", "Description", c => c.String(maxLength: 200));
            DropColumn("dbo.Recipes", "Name");
            DropTable("dbo.Ingridients");
        }
    }
}
