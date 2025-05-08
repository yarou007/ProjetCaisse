using System.ComponentModel.DataAnnotations;

namespace CustomAth.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public String Name{ get; set; }
    public ICollection<UserAccount> UserAccounts { get; set; }
    
    
}