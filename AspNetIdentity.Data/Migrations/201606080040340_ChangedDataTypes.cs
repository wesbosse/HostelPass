namespace AspNetIdentity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDataTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hostels", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hostels", "ZipCode", c => c.Int(nullable: false));
        }
    }
}
