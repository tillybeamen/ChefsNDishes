#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;

public class Chef
{
    [Key]
    public int ChefId {get;set;}

    [Required]
    [Display(Name = "First Name")]
    [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
    public string FirstName {get;set;}

    [Required]
    [Display(Name = "Last Name")]
    [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
    public string LastName {get;set;}

    [PastDate]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DOB {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public List<Dish> AllDishes {get;set;} = new List<Dish>();

}

public class PastDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value==null)
        {
            return new ValidationResult("Date of Birth is Required");
        }
        else if((DateTime)value >= DateTime.Now)
        {
            return new ValidationResult("Invalid date");
        }
        else
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            DateTime Date = (DateTime)value;
            int dob = int.Parse(Date.ToString("yyyyMMdd"));
            int age = (now-dob)/10000;
            if(age<18)
            {
                return new ValidationResult("Must be atleast 18 years old");
            }
            return ValidationResult.Success;
        }
    }
}