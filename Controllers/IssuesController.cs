using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bugtracker.Enums;
using bugtracker.Models;

namespace bugtracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly BugTrackerContext _context;

        public IssuesController(BugTrackerContext context)
        {
            _context = context;
            
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            if (_context.Issues != null) {
                return View(await _context.Issues.Where(issue => issue.Assigned == HttpContext.Session.GetString("Username")).ToListAsync());
            }else{
                return Problem("Entity set 'BugTrackerContext.Issues'  is null.");
            }
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Issues/Create
        public IActionResult Create()
        {
            if ( _context.Users == null)
            {
                return NotFound();
            }
            var issue = new Issue();
            issue.Users = _context.Users.Select(u => u.Username).ToList();
            return View(issue);
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Priority,DueDate,Assigned,Type")] Issue issue)
        {
            issue.CreatedBy = HttpContext.Session.GetString("Username");
            ModelState.Remove("Users");
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);

            issue.Users = _context.Users.Select(u => u.Username).ToList();
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Index", "Home");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
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
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
          return (_context.Issues?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void CheckStatus(ref Issue issue){
            if (issue != null){
                if (issue.Status == Status.COMPLETED){
                    issue.CompletedBy = HttpContext.Session.GetString("Username");
                }else{
                    issue.CompletedBy = "";
                }
            }
        }
    }
}
