using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace bugtracker.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string LastName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
    public string Password { get; set; }
    [NotMapped]
    [Required]
    [System.ComponentModel.DataAnnotations.Compare("Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
    public string Email { get; set; }
}
