using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bugtracker.Models;

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
        return status == "All" ? View(await _context.Issues.ToListAsync()) : View(await _context.Issues.Where(issue => issue.Status == status).ToListAsync());
    }
}
