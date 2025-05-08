using System.ComponentModel.DataAnnotations;

namespace CustomAth.Models;

public class RegistrationViewModel
{
    [Key]
    public int Id { get; set; }

    [Required (ErrorMessage = "First name is required")]
    [MaxLength(50,ErrorMessage = "Max 50 characters allowed")]
    
    public string FirstName { get; set; }
    
    
    [Required (ErrorMessage = "Last name is required")]
    [MaxLength(20,ErrorMessage = "Max 20 characters allowed")]

    public string LastName { get; set; }
    
    [Required (ErrorMessage = "User name is required")]
    [MaxLength(20,ErrorMessage = "Max 20 characters allowed")]

    public string UserName { get; set; }
    
    [Required (ErrorMessage = "Email name is required")]
    [MaxLength(50,ErrorMessage = "Max 20 characters allowed")]
    [EmailAddress(ErrorMessage = "Please enter a valid email.")]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
    
    public string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(20,MinimumLength = 5,ErrorMessage = "Max 20 or 5 min characters allowed")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password",ErrorMessage = "Pleace Confirm your password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}