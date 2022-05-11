using System.ComponentModel.DataAnnotations;
namespace bugtracker.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public List<Issue> Issues = new List<Issue>();
}
