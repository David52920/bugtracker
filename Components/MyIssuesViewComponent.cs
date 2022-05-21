using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using bugtracker.Models;
using bugtracker.Enums;

namespace bugtracker.Components;

public class MyIssuesViewComponent : ViewComponent
{
    private readonly BugTrackerContext _context;

    public MyIssuesViewComponent (BugTrackerContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string status)
    {
        if (status == null){
            status = "ALL";
        }
        var currentStatus = Status.PENDING;
        switch(status){
            case "PENDING":
                currentStatus = Status.PENDING;
                break;
            case "IN PROGRESS":
                currentStatus = Status.INPROGRESS;
                break;
            case "COMPLETED":
                currentStatus = Status.COMPLETED;
                break;
        }
        return status == "ALL" ? View(await _context.Issues.Where(issue => issue.Assigned == HttpContext.User.FindFirstValue("UserName")).ToListAsync()) : 
                                View(await _context.Issues.Where(issue => issue.Assigned == HttpContext.User.FindFirstValue("UserName") && issue.Status == currentStatus).ToListAsync());
    }
    
}
