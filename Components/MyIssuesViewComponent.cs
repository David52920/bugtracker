using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bugtracker.Models;

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
            status = "All";
        }
        return status == "All" ? View(await _context.Issues.Where(issue => issue.Assigned == HttpContext.Session.GetString("Username")).ToListAsync()) : 
                                View(await _context.Issues.Where(issue => issue.Assigned == HttpContext.Session.GetString("Username") && issue.Status == status).ToListAsync());
    }
    
}
