namespace hostL.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "HostLUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "Reservation_ReservationId", "dbo.Reservations");
            DropIndex("dbo.Payments", new[] { "HostLUser_Id" });
            DropIndex("dbo.Ratings", new[] { "Reservation_ReservationId" });
            DropColumn("dbo.Payments", "HostLUser_Id");
            DropColumn("dbo.Ratings", "Reservation_ReservationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Reservation_ReservationId", c => c.Int());
            AddColumn("dbo.Payments", "HostLUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ratings", "Reservation_ReservationId");
            CreateIndex("dbo.Payments", "HostLUser_Id");
            AddForeignKey("dbo.Ratings", "Reservation_ReservationId", "dbo.Reservations", "ReservationId");
            AddForeignKey("dbo.Payments", "HostLUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
