using AspNetIdentity.Core.Domain;

namespace AspNetIdentity.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetIdentity.Data.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AspNetIdentity.Data.Infrastructure.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(ir => ir.Name,
                new Role { Name = "Admin" },
                new Role { Name = "HostelOwner" },
                new Role { Name = "Traveller" });

            context.SaveChanges();
        }
    }
}
