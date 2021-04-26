namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ingridients = c.String(maxLength: 200),
                        Quantity = c.String(),
                        Description = c.String(maxLength: 200),
                        Status = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Categories", "UserId", "dbo.Users");
            DropForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropIndex("dbo.Recipes", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Recipes");
            DropTable("dbo.Categories");
        }
    }
}
