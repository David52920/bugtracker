using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using bugtracker.Enums;

namespace bugtracker.Models;

public partial class Issue
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    [Required]
    public IssueType Type { get; set; }
    [Required]
    public Status Status { get; set; }
    public string? CreatedBy { get; set; }
    public string? Assigned { get; set; }
    public string? CompletedBy { get; set; }
}

public partial class Issue
{
    [NotMapped]
    public List<string> Users { get; set; }
}