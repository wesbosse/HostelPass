namespace HostLPass.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEmailAddressAndAvailability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hostels", "Availability", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hostels", "Availability");
        }
    }
}
