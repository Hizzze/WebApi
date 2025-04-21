using App.Configurations;
using App.Models;
using App.Models.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.Database;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<User> Users { get; init; }
    public DbSet<Property> Properties { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurations()); 
        base.OnModelCreating(modelBuilder);
    }
}