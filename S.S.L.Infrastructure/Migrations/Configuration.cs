namespace S.S.L.Infrastructure.Migrations
{
    using global::S.S.L.Domain.Enums;
    using global::S.S.L.Infrastructure.S.S.L.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Entities>
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

            context.Users.AddOrUpdate(r => r.Email,
                new User
                {
                    FirstName = "Hillprieston",
                    LastName = "Okwara",
                    Email = "hillprieston@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "5f4dcc3b5aa765d61d8327deb882cf99",
                    UserType = UserType.Administrator
                });


            context.Roles.AddOrUpdate(r => r.Name,
                new Role { Name = "Administrator" },
                new Role { Name = "Facilitator" },
                new Role { Name = "Mentee" }

            );
            context.Facilitators.AddOrUpdate(r => r.UserId,
               new Facilitator
               {
                   UserId = 1,
               });
            context.UserRoles.AddOrUpdate(

               new UserRole
               {
                   UserId = 1,
                   RoleId = 1
               }
             );

            context.Countries.AddOrUpdate(r => r.Name,
                new Country { Name = "Nigeria" },
                new Country { Name = "Whales" },
                new Country { Name = "Ghana" }
                );

            var states = new List<string>() { "Abia", "Adamawa", "Akwa-Ibom" };

            states.ForEach(x =>
                context.States.AddOrUpdate(s => s.Name,
                    new State { Name = x, CountryId = 1 }));


        }
    }
}
