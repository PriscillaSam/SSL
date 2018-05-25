namespace S.S.L.Infrastructure.S.S.L.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Threading;
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
        public DbSet<Facilitator> Facilitators { get; set; }
        public DbSet<Forum> Fora { get; set; }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        public override int SaveChanges()
        {
            AddOrUpdateTime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            AddOrUpdateTime();
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

        private void AddOrUpdateTime()
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

