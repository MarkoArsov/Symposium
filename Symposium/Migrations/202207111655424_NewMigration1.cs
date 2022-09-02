namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserName", c => c.String());
            DropColumn("dbo.Posts", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "UserName");
        }
    }
}
