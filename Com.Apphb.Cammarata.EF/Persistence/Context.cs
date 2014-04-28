using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Apphb.Cammarata.EF.Model;

namespace Com.Apphb.Cammarata.EF.Persistence
{
    public class Context : DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
