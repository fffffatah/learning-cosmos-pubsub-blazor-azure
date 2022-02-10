using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using EntityLayer;

namespace DataAccessLayer
{
    public class CosmosContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var accountEndpoint = Environment.GetEnvironmentVariable("COSMOS_URL");
            var accountKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DB");
            optionsBuilder.UseCosmos(accountEndpoint, accountKey, dbName);
        }
        //public CosmosContext(DbContextOptions<CosmosContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToContainer("users");
        }
    }
}
