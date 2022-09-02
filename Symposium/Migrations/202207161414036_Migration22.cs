namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserNames", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.UserNames", new[] { "Post_PostId" });
            AddColumn("dbo.Posts", "LikedBy", c => c.String());
            DropTable("dbo.UserNames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserNames",
                c => new
                    {
                        UserNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.UserNameId);
            
            DropColumn("dbo.Posts", "LikedBy");
            CreateIndex("dbo.UserNames", "Post_PostId");
            AddForeignKey("dbo.UserNames", "Post_PostId", "dbo.Posts", "PostId");
        }
    }
}
