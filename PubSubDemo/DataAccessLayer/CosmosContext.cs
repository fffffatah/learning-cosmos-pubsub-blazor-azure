using Microsoft.EntityFrameworkCore;
using EntityLayer;

namespace DataAccessLayer
{
    public class CosmosContext : DbContext
    {
        public DbSet<Message>? Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var accountEndpoint = Environment.GetEnvironmentVariable("COSMOS_URL");
            var accountKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DB");
            optionsBuilder.UseCosmos(accountEndpoint, accountKey, dbName);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>().ToContainer("messages");
        }
    }
}