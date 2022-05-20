using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace bugtracker.Models;

public class ApplicationUser: IdentityUser
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string LastName { get; set; }
    [NotMapped]
    [Required]
    [System.ComponentModel.DataAnnotations.Compare("Password")]
    public string ConfirmPassword { get; set; }

    
}
