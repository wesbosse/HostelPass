namespace AspNetIdentity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HostLUsers", "EmailAddress", c => c.String());
            AlterColumn("dbo.HostLUsers", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.HostLUsers", "LastName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HostLUsers", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.HostLUsers", "FirstName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.HostLUsers", "EmailAddress");
        }
    }
}
