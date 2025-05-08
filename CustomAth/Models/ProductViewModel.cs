using System.ComponentModel.DataAnnotations;

namespace CustomAth.Models;

public class ProductViewModel
{
    [Key]
    public int Id { get; set; }
    
    [Required (ErrorMessage = "Product name is required")]
    [MaxLength(20,ErrorMessage = "Max 50 characters allowed")]
    public string ProductName { get; set; }
    [Required (ErrorMessage = "Product Price is required")]

    public decimal ProductUnitPrice { get; set; }
    [Required (ErrorMessage = "Product Quantity is required")]

    public int ProductQuantity { get; set; }
    
    
    public int CategoryId { get; set; }
}