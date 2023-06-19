#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;

public class Dish
{
    [Key]
    public int DishId {get;set;}

    [Required]
    [MinLength(2, ErrorMessage = "Name must be atleast 2 characters")]
    public string Name {get;set;}

    [Required]
    [Range(1,int.MaxValue, ErrorMessage = "Calories must be atleast 1")]
    public int? Calories {get;set;}

    [Required]
    [Range(1,5)]
    public int Tastiness {get;set;}

    [Required(ErrorMessage =  "Chef Details missing")]
    public int? ChefId {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public Chef? Chef {get;set;}
}