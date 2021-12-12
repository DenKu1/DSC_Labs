using System.ComponentModel.DataAnnotations;

namespace RabbitMQSweets.WebUI.Models;

public class CandyModel
{
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
    
    [Required]
    [MinLength(5)]
    public string Production { get; set; }
    
    [Required]
    public string Ingredients { get; set; }
    
    [Required]
    public int Energy { get; set; }
    
    [Required]
    public int Proteins { get; set; }
    
    [Required]
    public int Fats { get; set; }
    
    [Required]
    public int Carbohydrates { get; set; }
    
    [Required]
    public string CandyTypes { get; set; }
}