namespace S.S.L.Infrastructure.Migrations
{
    using global::S.S.L.Domain.Enums;
    using global::S.S.L.Domain.Interfaces.Utilities;
    using global::S.S.L.Infrastructure.S.S.L.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Entities>
    {
        public Configuration(IEncryption encryption)
        {
            AutomaticMigrationsEnabled = false;
            _encrypt = encryption;
        }

        public IEncryption _encrypt { get; }

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
                    PasswordHash = _encrypt.Encrypt("password"),
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

            var countries = new List<string>()
            { "Nigeria", "Whales", "Ghana", "Nairobi", "Mozambique" };

            countries.ForEach(c =>
                context.Countries.AddOrUpdate(country => country.Name,
                     new Country { Name = c }));


            var states = new List<string>()
            { "Abia", "Adamawa", "Akwa-Ibom", "Anambra", "Bauchi", "Bayelsa", "Benue", "Borno", "Delta", "Ebonyi", "Enugu", "Cross River", "Edo", "Ekiti", "Gombe", "Imo", "Jigawa", "Kaduna", "Kano", "Katsina", "Kebbi", "Kogi", "Kwara", "Lagos", "Nasarawa", "Niger", "Ogun", "Ondo", "Osun", "Oyo", "Plateau", "Rivers", "Sokoto", "Taraba", "Yobe", "Zamfara", "F.C.T" };

            states.ForEach(x =>
                context.States.AddOrUpdate(s => s.Name,
                    new State { Name = x, CountryId = 1 }));


        }
    }
}
