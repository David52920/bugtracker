namespace bugtracker.Enums;
using System.ComponentModel.DataAnnotations;

public enum Status : ushort{
    Pending = 0,
    [Display(Name="In Progress")]
    InProgress = 1,
    Completed = 2
}
