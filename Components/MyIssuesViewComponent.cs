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

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _context.Issues.Where(issue => issue.StartedBy == HttpContext.Session.GetString("Username")).ToListAsync());
    }
}
