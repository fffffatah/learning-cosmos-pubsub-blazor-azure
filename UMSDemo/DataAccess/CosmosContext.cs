using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;

namespace DataAccess
{
    public class CosmosContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<EntityLayer.File>? Files { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var accountEndpoint = Environment.GetEnvironmentVariable("COSMOS_URL");
            var accountKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DB");
            optionsBuilder.UseCosmos(accountEndpoint, accountKey, dbName);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultContainer("demo");
            builder.Entity<User>().ToContainer("demo");
            builder.HasDefaultContainer("files");
            builder.Entity<EntityLayer.File>().ToContainer("files");
            //builder.Entity<User>().HasPartitionKey("/users");
        }
    }
}