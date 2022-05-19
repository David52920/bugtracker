namespace bugtracker.Enums;
using System.ComponentModel.DataAnnotations;

public enum Status : ushort{
    PENDING = 0,
    [Display(Name="IN PROGRESS")]
    INPROGRESS = 1,
    COMPLETED = 2
}
