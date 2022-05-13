using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
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
        if (_context != null){
            if (HttpContext.Session.GetString("Username") != null){
                HttpContext.Session.SetString("AuthenticatedUser", "true");
                DateTime date = DateTime.Now;
                string timeOfDay = date.TimeOfDay > new TimeSpan(11, 59, 00) ? "afternoon" : "morning";
                ViewBag.Date = $"{date.DayOfWeek}, May {date.Day}";
                ViewBag.Greeting = $"Good {timeOfDay}, {HttpContext.Session.GetString("FirstName")}";
                return View();
            }else{
                return RedirectToAction("Login");
            }
        }else{
            return Problem();
        }
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User _user)
    {
        if (ModelState.IsValid)
        {
            var check = _context.Users.FirstOrDefault(s => s.Username == _user.Username || s.Email == _user.Email);
            if (check == null)
            {
                _user.Password = GetMD5(_user.Password);
                _context.Users.Add(_user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Email already exists";
                return View();
            }
        }
        return View();
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(string username,string password)
    {
        if (ModelState.IsValid)
        {
            var f_password = GetMD5(password);
            var data =_context.Users.Where(s => s.Username.Equals(username) && s.Password.Equals(f_password)).ToList();
            if (data.Count() > 0)
            {
                HttpContext.Session.SetString("FullName", data.FirstOrDefault().FirstName +" "+ data.FirstOrDefault().LastName);
                HttpContext.Session.SetString("FirstName", data.FirstOrDefault().FirstName);
                HttpContext.Session.SetString("Email", data.FirstOrDefault().Email);
                HttpContext.Session.SetString("Username", data.FirstOrDefault().Username);
                HttpContext.Session.SetString("UserID", data.FirstOrDefault().Id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }
        }
        return View();
    }

    public ActionResult Logout()
    {
        HttpContext.Session.SetString("AuthenticatedUser", "false");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public static string GetMD5(string str)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] fromData = Encoding.UTF8.GetBytes(str);
        byte[] targetData = md5.ComputeHash(fromData);
        string byte2String = null;

        for (int i = 0; i < targetData.Length; i++)
        {
            byte2String += targetData[i].ToString("x2");

        }
        return byte2String;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
