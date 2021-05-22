using keyapi.Maps;
using Microsoft.EntityFrameworkCore;
using System;

namespace keyapi.Models{
    /// <summary>Api Key Database Context</summary>
    public class ApiKeyDbContext : DbContext
    {
        public ApiKeyDbContext(DbContextOptions<ApiKeyDbContext> options)
       : base(options)
        {

        }
        public DbSet<ApiKey> ApiKeys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ApiKeyMap(modelBuilder.Entity<ApiKey>());
        }
    }
}