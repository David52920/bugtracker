using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
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
        HttpContext.Session.SetString("Username", "DR");
        DateTime date = DateTime.Now;
        string timeOfDay = date.TimeOfDay > new TimeSpan(11, 59, 00) ? "afternoon" : "morning";
        ViewBag.User = $"drees" ;
        ViewBag.Date = $"{date.DayOfWeek}, May {date.Day}";
        ViewBag.Greeting = $"Good {timeOfDay}, {ViewBag.User}";
        if (_context.Issue != null){
            return View();
        }else{
            return Problem("Entity set 'BugTrackerContext.Issue'  is null.");
        }
    }

    public async Task<ActionResult> MyIssuesPartial()
    {
        return ViewComponent("Index", await _context.Issue.Where(issue => issue.StartedBy == TempData["Username"]).ToListAsync());
    }

    public async Task<ActionResult> AllIssuesPartial()
    {
        return ViewComponent("Index", await _context.Issue.ToListAsync());
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
