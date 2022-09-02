namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Likes");
        }
    }
}
