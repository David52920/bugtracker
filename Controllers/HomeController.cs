using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bugtracker.Models;

namespace bugtracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BugTrackerContext _context;

    public HomeController(ILogger<HomeController> logger, BugTrackerContext context)
    {
        _logger = logger;
        _context = context;
    }

    // GET: Issues
    public async Task<IActionResult> Index()
    {
        return _context.Issue != null ? 
                    View(await _context.Issue.ToListAsync()) :
                    Problem("Entity set 'BugTrackerContext.Issue'  is null.");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
