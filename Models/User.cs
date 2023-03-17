#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Display(Name = "First Name")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    [Required(ErrorMessage =" is required")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    [Required(ErrorMessage =" is required")]
    public string LastName { get; set; }
    [Required(ErrorMessage =" is required")]
    [MinLength(3, ErrorMessage ="must be at least 3 characters")]
    [MaxLength(15)]
    [UniqueEmail]
    public string Email { get; set; }
    [MinLength(8, ErrorMessage = "must be at least 8 characters")]
    [Required(ErrorMessage =" is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage =" do not match")]
    [Required(ErrorMessage =" is required")]
    [Display(Name = "Confirm Password")]
    public string PasswordConfirm { get; set; }  
    public List<Workout> CreatedWorkouts { get; set; } = new List<Workout>();
    public List<Favorite> FavoriteWorkouts { get; set; } = new List<Favorite>();  
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Though we have Required as a validation, sometimes we make it here anyways
        // In which case we must first verify the value is not null before we proceed
        if (value == null)
        {
            // If it was, return the required error
            return new ValidationResult("Email is required!");
        }

        // This will connect us to our database since we are not in our Controller
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        // Check to see if there are any records of this email in our database
        if (_context.Users.Any(e => e.Email == value.ToString()))
        {
            // If yes, throw an error
            return new ValidationResult("Email is already in use!");
        }
        else
        {
            // If no, proceed
            return ValidationResult.Success;
        }
    }
}

