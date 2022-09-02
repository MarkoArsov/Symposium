namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserNames",
                c => new
                    {
                        UserNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.UserNameId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => t.Post_PostId);
            
            DropColumn("dbo.Posts", "LikedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "LikedBy", c => c.String());
            DropForeignKey("dbo.UserNames", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.UserNames", new[] { "Post_PostId" });
            DropTable("dbo.UserNames");
        }
    }
}
