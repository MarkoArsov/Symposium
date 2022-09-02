namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "Post_PostId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Post_PostId");
            AddForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts", "PostId");
            DropColumn("dbo.Posts", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId" });
            DropColumn("dbo.AspNetUsers", "Post_PostId");
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
