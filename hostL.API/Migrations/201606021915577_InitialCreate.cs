namespace hostL.API.Migrations
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
                        UserId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Image = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.HostelId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ReservationId = c.Int(nullable: false),
                        Subject = c.String(),
                        Text = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                        HostLUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .ForeignKey("dbo.AspNetUsers", t => t.HostLUser_Id)
                .Index(t => t.ReservationId)
                .Index(t => t.HostLUser_Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        HostelId = c.Int(),
                        TravellerId = c.String(maxLength: 128),
                        Cleanliness = c.Int(nullable: false),
                        Atmosphere = c.Int(nullable: false),
                        Location = c.Int(nullable: false),
                        Friendliness = c.Int(nullable: false),
                        Recommend = c.Int(nullable: false),
                        Communication = c.Int(nullable: false),
                        AverageRating = c.Int(nullable: false),
                        Reservation_ReservationId = c.Int(),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Reservations", t => t.Reservation_ReservationId)
                .ForeignKey("dbo.AspNetUsers", t => t.TravellerId)
                .ForeignKey("dbo.Hostels", t => t.HostelId)
                .Index(t => t.HostelId)
                .Index(t => t.TravellerId)
                .Index(t => t.Reservation_ReservationId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservations", "HostelId", "dbo.Hostels");
            DropForeignKey("dbo.Ratings", "HostelId", "dbo.Hostels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "TravellerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "Reservation_ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Payments", "HostLUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Messages", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Hostels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.HostelAmenity", "Amenity_AmenityId", "dbo.Amenities");
            DropForeignKey("dbo.HostelAmenity", "Hostel_HostelId", "dbo.Hostels");
            DropIndex("dbo.HostelAmenity", new[] { "Amenity_AmenityId" });
            DropIndex("dbo.HostelAmenity", new[] { "Hostel_HostelId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "Reservation_ReservationId" });
            DropIndex("dbo.Ratings", new[] { "TravellerId" });
            DropIndex("dbo.Ratings", new[] { "HostelId" });
            DropIndex("dbo.Payments", new[] { "HostLUser_Id" });
            DropIndex("dbo.Payments", new[] { "ReservationId" });
            DropIndex("dbo.Reservations", new[] { "HostelId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ReservationId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Hostels", new[] { "UserId" });
            DropTable("dbo.HostelAmenity");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.Payments");
            DropTable("dbo.Reservations");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Hostels");
            DropTable("dbo.Amenities");
        }
    }
}
