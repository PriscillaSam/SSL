using S.S.L.Infrastructure.Migrations;
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
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }
    }
}