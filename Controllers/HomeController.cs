using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using bugtracker.Enums;
using bugtracker.Models;

namespace bugtracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BugTrackerContext _context;
    private readonly INotyfService _notifyService;

    public HomeController(ILogger<HomeController> logger, BugTrackerContext context, INotyfService notifyService)
    {
        _logger = logger;
        _context = context;
        _notifyService = notifyService;
    }

    [Authorize]
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated){
            if (_context != null){
                var userId = this.User.FindFirstValue("UserName");
                DateTime date = DateTime.Now;
                string month = date.ToString("MMMM");
                string timeOfDay = date.TimeOfDay > new TimeSpan(11, 59, 00) ? "afternoon" : "morning";
                ViewBag.Date = $"{date.DayOfWeek}, {month} {date.Day}";
                ViewBag.Greeting = $"Good {timeOfDay}, {User.FindFirstValue("FirstName")}";
                ViewBag.PendingCount = _context.Issues.Count(issue => issue.Assigned == userId && issue.Status == Status.PENDING);
                ViewBag.InProgressCount = _context.Issues.Count(issue => issue.Assigned == userId && issue.Status == Status.INPROGRESS);
                ViewBag.CompletedCount = _context.Issues.Count(issue => issue.Assigned == userId && issue.Status == Status.COMPLETED);
            }
            return View();
        }else{
            return RedirectToAction("Login", "Account");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ReloadAll(string status){
        return ViewComponent("AllIssues", status);
    }

    [HttpPost]
    public IActionResult ReloadMy(string status){
        return ViewComponent("MyIssues", status);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
