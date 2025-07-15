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
    public DbSet<PropertyImage> PropertyImages { get; init; }
    
    public DbSet<Favorite> Favorites { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurations()); 
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyImageConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}