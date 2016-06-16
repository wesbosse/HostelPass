namespace HostLPass.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amenities",
                c => new
                    {
                        AmenityId = c.Int(nullable: false, identity: true),
                        TextAmenity = c.String(),
                    })
                .PrimaryKey(t => t.AmenityId);
            
            CreateTable(
                "dbo.Hostels",
                c => new
                    {
                        HostelId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        HostelName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Description = c.String(),
                        Phone = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.HostelId)
                .ForeignKey("dbo.HostLUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.HostLUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Phone = c.String(),
                        EmailAddress = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        HostelOwner = c.Boolean(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        ReservationId = c.Int(nullable: false),
                        Subject = c.String(),
                        Text = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .ForeignKey("dbo.HostLUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        HostelId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        GuestNum = c.Int(nullable: false),
                        ConfirmedStatus = c.Boolean(nullable: false),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.HostLUsers", t => t.UserId)
                .ForeignKey("dbo.Hostels", t => t.HostelId)
                .Index(t => t.UserId)
                .Index(t => t.HostelId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        HostelId = c.Int(),
                        TravellerId = c.Int(),
                        Cleanliness = c.Int(nullable: false),
                        Atmosphere = c.Int(nullable: false),
                        Location = c.Int(nullable: false),
                        Friendliness = c.Int(nullable: false),
                        Recommend = c.Int(nullable: false),
                        Communication = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.HostLUsers", t => t.TravellerId)
                .ForeignKey("dbo.Hostels", t => t.HostelId)
                .Index(t => t.HostelId)
                .Index(t => t.TravellerId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.HostLUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HostelAmenity",
                c => new
                    {
                        Hostel_HostelId = c.Int(nullable: false),
                        Amenity_AmenityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hostel_HostelId, t.Amenity_AmenityId })
                .ForeignKey("dbo.Hostels", t => t.Hostel_HostelId)
                .ForeignKey("dbo.Amenities", t => t.Amenity_AmenityId)
                .Index(t => t.Hostel_HostelId)
                .Index(t => t.Amenity_AmenityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "HostelId", "dbo.Hostels");
            DropForeignKey("dbo.Ratings", "HostelId", "dbo.Hostels");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.HostLUsers");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.HostLUsers");
            DropForeignKey("dbo.Ratings", "TravellerId", "dbo.HostLUsers");
            DropForeignKey("dbo.Messages", "UserId", "dbo.HostLUsers");
            DropForeignKey("dbo.Payments", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Messages", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Hostels", "UserId", "dbo.HostLUsers");
            DropForeignKey("dbo.HostelAmenity", "Amenity_AmenityId", "dbo.Amenities");
            DropForeignKey("dbo.HostelAmenity", "Hostel_HostelId", "dbo.Hostels");
            DropIndex("dbo.HostelAmenity", new[] { "Amenity_AmenityId" });
            DropIndex("dbo.HostelAmenity", new[] { "Hostel_HostelId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "TravellerId" });
            DropIndex("dbo.Ratings", new[] { "HostelId" });
            DropIndex("dbo.Payments", new[] { "ReservationId" });
            DropIndex("dbo.Reservations", new[] { "HostelId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ReservationId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Hostels", new[] { "UserId" });
            DropTable("dbo.HostelAmenity");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.Payments");
            DropTable("dbo.Reservations");
            DropTable("dbo.Messages");
            DropTable("dbo.HostLUsers");
            DropTable("dbo.Hostels");
            DropTable("dbo.Amenities");
        }
    }
}
