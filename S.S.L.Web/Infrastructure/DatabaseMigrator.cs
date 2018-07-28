using S.S.L.Domain.Interfaces.Utilities;
using S.S.L.Infrastructure.Migrations;
using S.S.L.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace S.S.L.Web.Infrastructure
{
    public class DatabaseMigrator
    {

        public static void UpdateDatabase()
        {
            var migrator = new DbMigrator(new Configuration(new MD5Encryption()));
            migrator.Update();
        }
    }
}