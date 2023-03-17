#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[NotMapped]
public class LogUser{
    [Required(ErrorMessage = "is required")]
    [Display(Name = "Email address")]
    [EmailAddress(ErrorMessage ="not a valid email")]
    public string LogEmail {get;set;}
    [Required(ErrorMessage = "is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string LogPassword {get;set;}
}