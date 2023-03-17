#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models;

public class Favorite{
    [Key]
    public int FavoriteId { get; set; }

    public int UserId { get;set; }
    public User? User { get; set; }
    public int WorkoutId { get;set; }
    public Workout? Workout { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}