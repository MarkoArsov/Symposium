namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId" });
            DropColumn("dbo.AspNetUsers", "Post_PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Post_PostId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Post_PostId");
            AddForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts", "PostId");
        }
    }
}
