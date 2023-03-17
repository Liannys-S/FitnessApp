#pragma warning disable CS8618
namespace FitnessApp.Models;

public class MyViewModel
{
    public List<Workout> myWorkouts {get;set;} = new List<Workout>();
    public List<Favorite> myFavorites {get;set;} = new List<Favorite>();

}

