using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bugtracker.Models{
    public class RegisterViewModel {
        [Required]
        [Display(Name ="Username:")]
        public string UserName { get; set; }
        [Required]
        [Display(Name ="First Name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last Name:")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name ="Password:")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password:")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password:")]
        [Compare("Password",ErrorMessage ="Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
    }

}