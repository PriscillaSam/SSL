using Ninject.Modules;
using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Interfaces.Utilities;
using S.S.L.Infrastructure.Repositories;
using S.S.L.Infrastructure.S.S.L.Entities;
using S.S.L.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace S.S.L.Web.Infrastructure.NinjectModules
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<Entities>();
            Bind<IEncryption>().To<MD5Encryption>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IStateCountryRepository>().To<StateCountryRepository>();
        }
    }
}