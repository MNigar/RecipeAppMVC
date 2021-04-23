namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recipes");
        }
    }
}
