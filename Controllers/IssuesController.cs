using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using bugtracker.Enums;
using bugtracker.Models;

namespace bugtracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly BugTrackerContext _context;
        private readonly INotyfService _notifyService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IssuesController(BugTrackerContext context, INotyfService notifyService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _notifyService = notifyService;
            _userManager = userManager;
        }

        // GET: Issues
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _context.Issues != null ? View(await _context.Issues.Where(issue => issue.Assigned == User.FindFirstValue("UserName")).ToListAsync()) :
                Problem("Entity set 'BugTrackerContext.Issues'  is null.");
        }

        // GET: Issues/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        [Authorize]
        public IActionResult Create()
        {
            if ( _context == null)
            {
                return NotFound();
            }
            var issue = new Issue(){
                Users = (from u in _userManager.Users select u.UserName).ToList(),
                DueDate=  DateTime.Now
            };

            return View(issue);
        }

        // POST: Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Priority,DueDate,Assigned,Type")] Issue issue)
        {
            issue.CreatedBy = User.FindFirstValue("UserName");
            ModelState.Remove("Users");
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                _notifyService.Success("Created Issue, ID: " + issue.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);

            issue.Users = (from u in _userManager.Users select u.UserName).ToList();
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Priority,DueDate,Type,Status,CreatedBy,CompletedBy,Assigned")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Users");
            if (ModelState.IsValid)
            {
                try
                {
                    CheckStatus(ref issue);
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyService.Success("Edited Issue, ID: " + issue.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditModal(int id, [Bind("Id,Name,Description,Priority,DueDate,Type,Status,CreatedBy,CompletedBy,Assigned")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Users");
            if (ModelState.IsValid)
            {
                try
                {
                    CheckStatus(ref issue);
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyService.Success("Edited Issue, ID: " + issue.Id);
                return RedirectToAction("Index", "Home");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Issues == null)
            {
                return Problem("Entity set 'BugTrackerContext.Issues'  is null.");
            }
            var issue = await _context.Issues.FindAsync(id);
            if (issue != null)
            {
                _context.Issues.Remove(issue);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Deleted Issue, ID: " + issue.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
          return (_context.Issues?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void CheckStatus(ref Issue issue){
            if (issue != null){
                if (issue.Status == Status.COMPLETED){
                    issue.CompletedBy = User.FindFirstValue("UserName");
                }else{
                    issue.CompletedBy = "";
                }
            }
        }
    }
}
