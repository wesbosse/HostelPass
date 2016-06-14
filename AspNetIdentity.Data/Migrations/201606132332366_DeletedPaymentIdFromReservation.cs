namespace AspNetIdentity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedPaymentIdFromReservation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "PaymentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "PaymentId", c => c.Int(nullable: false));
        }
    }
}
