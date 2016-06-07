namespace AspNetIdentity.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedByteArray : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hostels", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hostels", "Image", c => c.Binary());
        }
    }
}
