using System.ComponentModel.DataAnnotations;
using bugtracker.Enums;

namespace bugtracker.Models;

public class Issue
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? StartedBy { get; set; }
    public string? Assigned { get; set; }
    public string? CompletedBy { get; set; }
}
