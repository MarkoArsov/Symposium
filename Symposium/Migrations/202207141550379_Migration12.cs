namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImgURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImgURL");
        }
    }
}
