#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace FitnessApp.Models;

public class MyContext : DbContext 
{    
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<User> Users { get; set; } 
    public DbSet<Workout> Workouts { get; set; } 
    public DbSet<Favorite> Favorites { get; set; } 
}

