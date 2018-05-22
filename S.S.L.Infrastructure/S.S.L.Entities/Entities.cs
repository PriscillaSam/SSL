namespace S.S.L.Infrastructure.S.S.L.Entities
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Entities : DbContext
    {
        // Your context has been configured to use a 'Entities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'S.S.L.Infrastructure.S.S.L.Entities.Entities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Entities' 
        // connection string in the application configuration file.
        public Entities() : base("name=Entities")
        {
        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Facilitator> Facilitators { get; set; }
        public DbSet<Forum> Fora { get; set; }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }


    }

}