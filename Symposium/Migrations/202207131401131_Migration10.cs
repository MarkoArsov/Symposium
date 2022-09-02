namespace Symposium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddToRoleViewModels",
                c => new
                    {
                        AddToRoleViewModelID = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.AddToRoleViewModelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AddToRoleViewModels");
        }
    }
}
