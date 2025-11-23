using Microsoft.EntityFrameworkCore;
using MongoDb.Entity;
using MongoDB.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MongoDb;

public class MongoDbContext : DbContext
{
    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<User>().ToCollection("Users");
        _ = modelBuilder.Entity<User>().HasKey(u => u.Id);
        _ = modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValue(0);
    }
}





