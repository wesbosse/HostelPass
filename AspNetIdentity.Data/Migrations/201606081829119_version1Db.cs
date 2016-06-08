namespace AspNetIdentity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version1Db : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hostels", "Phone", c => c.String());
            AddColumn("dbo.HostLUsers", "Phone", c => c.String());
            AddColumn("dbo.HostLUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HostLUsers", "EmailConfirmed");
            DropColumn("dbo.HostLUsers", "Phone");
            DropColumn("dbo.Hostels", "Phone");
        }
    }
}
