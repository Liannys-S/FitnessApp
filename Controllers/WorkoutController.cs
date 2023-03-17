using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FitnessApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using static FitnessApp.Controllers.UserController;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers;

public class WorkoutController : Controller
{

    private MyContext db;  // or use _context instead of db

    public WorkoutController(MyContext context)
    {
        db = context; // if you use _context above use it here too
    }

    [SessionCheck]
    [HttpGet("workout/new")]
    public IActionResult New()
    {
        return View("New");
    }

    [SessionCheck]
    [HttpPost("workout/create")]
    public IActionResult Create(Workout newWorkout)
    {
        if(!ModelState.IsValid){
            return View("New");
        }
        newWorkout.UserId = (int)HttpContext.Session.GetInt32("UserId");
        db.Workouts.Add(newWorkout);
        db.SaveChanges();
        return RedirectToAction("All");
    }

    [SessionCheck]
    [HttpGet("workout/all")]
    public IActionResult All(string search)
    {
        if(search is not null){
            Console.WriteLine("search");
            List<Workout> filteredWorkouts = db.Workouts.Include(w => w.Author).Where(w => w.Title.Contains(search)).ToList();
            return View("All", filteredWorkouts);
        }
        Console.WriteLine("not search");
        List<Workout> allWorkouts = db.Workouts.Include(w => w.Author).ToList();
        return View("All", allWorkouts);
    }


    [SessionCheck]
    [HttpGet("workout/{workoutId}")]
    public IActionResult ViewOne(int workoutId){
        Workout? workout = db.Workouts.Include(w => w.Author).Include(w => w.UserFavorites).FirstOrDefault(w => w.WorkoutId == workoutId);
        if(workout is null){
            return RedirectToAction("All");
        }
        return View("ViewOne", workout);
    }

    [SessionCheck]
    [HttpGet("workout/{workoutId}/edit")]
    public IActionResult Edit(int workoutId)
    {
        Workout? workout = db.Workouts.FirstOrDefault(w => w.WorkoutId == workoutId);
        if(workout is null || workout.UserId != HttpContext.Session.GetInt32("UserId")){
            return RedirectToAction("All");
        }
        return View("Edit", workout);
    }

    [SessionCheck]
    [HttpPost("workout/{workoutId}/update")]
    public IActionResult Update(Workout editedWorkout, int workoutId)
    {
        if(!ModelState.IsValid){
            return Edit(workoutId);
        }
        Workout? workout = db.Workouts.FirstOrDefault(w => w.WorkoutId == workoutId);
        if(workout is null || workout.UserId != HttpContext.Session.GetInt32("UserId")){
            return RedirectToAction("All");
        }
        workout.Title = editedWorkout.Title;
        workout.Body = editedWorkout.Body;
        workout.UpdatedAt = DateTime.Now;
        db.Workouts.Update(workout);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new{ workoutId = workoutId});
    }

    [SessionCheck]
    [HttpPost("workout/{workoutId}/delete")]
    public IActionResult Delete(int workoutId){
        Workout? workout = db.Workouts.FirstOrDefault(w => w.WorkoutId == workoutId);
        if(workout is not null && workout.UserId == HttpContext.Session.GetInt32("UserId")){
            db.Workouts.Remove(workout);
            db.SaveChanges();
        }
        return RedirectToAction("All");
    }

    [SessionCheck]
    [HttpGet("workout/my-workouts")]
    public IActionResult MyWorkouts(){
        MyViewModel? favAndCreated = new MyViewModel
        {
            myWorkouts = db.Workouts.Where(w => w.UserId == HttpContext.Session.GetInt32("UserId")).ToList(),
            myFavorites = db.Favorites.Include(w => w.Workout).Where(w => w.UserId == HttpContext.Session.GetInt32("UserId")).ToList()
        };
        return View("MyWorkouts", favAndCreated);
    }

    [SessionCheck]
    [HttpPost("workout/{workoutId}/favorite")]
    public IActionResult Favorite(int workoutId)
    {
        Favorite? favorite = db.Favorites.FirstOrDefault(f => f.UserId == (int)HttpContext.Session.GetInt32("UserId") && f.WorkoutId == workoutId);

        if(favorite is not null){
            db.Favorites.Remove(favorite);
        }else{
            Favorite newFav = new Favorite{
                UserId = (int)HttpContext.Session.GetInt32("UserId"),
                WorkoutId = workoutId
            };
            db.Favorites.Add(newFav);
        }
        db.SaveChanges();
        return RedirectToAction("ViewOne",new{ workoutId = workoutId});
    } 
}