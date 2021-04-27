namespace RecipeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewtablesh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingridients", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingridients", "Name", c => c.String());
        }
    }
}
