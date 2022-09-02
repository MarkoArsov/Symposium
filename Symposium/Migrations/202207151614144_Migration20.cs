namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LikedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "LikedBy");
        }
    }
}
