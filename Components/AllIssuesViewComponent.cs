using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bugtracker.Models;
using bugtracker.Enums;

namespace bugtracker.Components;

public class AllIssuesViewComponent : ViewComponent
{
    private readonly BugTrackerContext _context;

    public AllIssuesViewComponent (BugTrackerContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string status)
    {
        if (status == null){
            status = "All";
        }
        var currentStatus = Status.Pending;
        switch(status){
            case "Pending":
                currentStatus = Status.Pending;
                break;
            case "In Progress":
                currentStatus = Status.InProgress;
                break;
            case "Completed":
                currentStatus = Status.Completed;
                break;
        }
        return status == "All" ? View(await _context.Issues.ToListAsync()) : View(await _context.Issues.Where(issue => issue.Status == currentStatus).ToListAsync());
    }
}
