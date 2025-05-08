using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CustomAth.Models;


 
[Index(nameof(Email),IsUnique = true)]
[Index(nameof(UserName),IsUnique = true)]
public class UserAccount
{
    [Key]
    public int Id { get; set; }

    [Required (ErrorMessage = "First name is required")]
    [MaxLength(20,ErrorMessage = "Max 50 characters allowed")]
    
    public string FirstName { get; set; }
    
    
    [Required (ErrorMessage = "Last name is required")]
    [MaxLength(20,ErrorMessage = "Max 50 characters allowed")]

    public string LastName { get; set; }
    
    [Required (ErrorMessage = "User name is required")]
    [MaxLength(20,ErrorMessage = "Max 50 characters allowed")]

    public string UserName { get; set; }
    
    [Required (ErrorMessage = "Email name is required")]
    [MaxLength(20,ErrorMessage = "Max 20 characters allowed")]
    
    
    public string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [MaxLength(20,ErrorMessage = "Max 20 characters allowed")]

    public string Password { get; set; }
    
    
    public DateTime? timeStamp { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
    
}