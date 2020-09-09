using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bugHolder.Data;
using bugHolder.Models;

namespace bugHolder.Controllers
{
    public class BugsController : Controller
    {
        private readonly MvcBugContext _context;

        public BugsController(MvcBugContext context)
        {
            _context = context;
        }

        // GET: Bugs
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Bug.ToListAsync());
        //}

        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var bugs = from b in _context.Bug
        //               select b;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        bugs = bugs.Where(s => s.Title.Contains(searchString));
        //    }

        //    return View(await bugs.ToListAsync());
        //}

        // GET: BugsStatus
        public async Task<IActionResult> Index(string bugStatus, string searchString)
        {
            // Use LINQ to get list of statuses.
            IQueryable<string> statusQuery = from b in _context.Bug
                                             orderby b.Status
                                             select b.Status;

            var bugs = from b in _context.Bug
                       select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                bugs = bugs.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bugStatus))
            {
                bugs = bugs.Where(x => x.Status == bugStatus);
            }

            var bugStatusVM = new BugStatusViewModel
            {
                Statuses = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Bugs = await bugs.ToListAsync()
            };

            return View(bugStatusVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Status,Date")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Status,Date")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
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
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bug.FindAsync(id);
            _context.Bug.Remove(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return _context.Bug.Any(e => e.Id == id);
        }
    }
}
