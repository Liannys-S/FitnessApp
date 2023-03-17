#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

public class Workout{
    [Key]
    public int WorkoutId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
    public int UserId { get; set; }
    public User? Author { get; set; }
    public List<Favorite> UserFavorites { get; set; } = new List<Favorite>(); 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}