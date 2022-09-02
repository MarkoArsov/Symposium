namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Posts", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
