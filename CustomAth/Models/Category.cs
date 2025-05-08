using System.ComponentModel.DataAnnotations;

namespace CustomAth.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Category Name is required")]
    public string CategoryName { get; set; }
    
    public ICollection<Product> Products { get; set; }

}