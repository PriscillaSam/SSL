namespace S.S.L.Infrastructure.Migrations
{
    using global::S.S.L.Infrastructure.S.S.L.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<S.S.L.Entities.Entities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Method for seeding data into database. 
        /// This method is called every time Update-Database is run
        /// </summary>
        /// <param name="context"></param>
        
        protected override void Seed(Entities context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            
            context.Roles.AddOrUpdate(r => r.Name,
                new Role { Name = "Admin" },
                new Role { Name = "Facilitator" },
                new Role { Name = "Mentee" }

            );


        }
    }
}
