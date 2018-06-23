namespace S.S.L.Infrastructure.S.S.L.Entities
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

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
        public DbSet<Country> Countries { get; set; }
        public DbSet<Facilitator> Facilitators { get; set; }
        public DbSet<Forum> Fora { get; set; }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Thought> Thoughts { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        public override int SaveChanges()
        {
            AddOrUpdateDateTime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            AddOrUpdateDateTime();
            return base.SaveChangesAsync();
        }


        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Updates the createdAt and updatedAt fields of application database entities when entities are added or modified 
        /// </summary>
        private void AddOrUpdateDateTime()
        {

            var currentDateTime = DateTime.Now;
            var entities = ChangeTracker.Entries<BaseModel>();

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = currentDateTime;
                        entity.Entity.UpdatedAt = currentDateTime;
                        break;

                    case EntityState.Modified:
                        entity.Entity.UpdatedAt = currentDateTime;
                        break;


                }
            }



        }



    }

}

