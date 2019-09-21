using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PubHub.Data;
using PubHub.Models;

namespace PubHub.Controllers
{
    public class DrinkEnthusiastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrinkEnthusiastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DrinkEnthusiasts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DrinkEnthusiasts.Include(d => d.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DrinkEnthusiasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkEnthusiast = await _context.DrinkEnthusiasts
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DrinkEnthusiastId == id);
            if (drinkEnthusiast == null)
            {
                return NotFound();
            }

            return View(drinkEnthusiast);
        }

        // GET: DrinkEnthusiasts/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: DrinkEnthusiasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrinkEnthusiastId,FirstName,LastName,Address,City,State,Zipcode,CheckInStatus,ApplicationId")] DrinkEnthusiast drinkEnthusiast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkEnthusiast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
            return View(drinkEnthusiast);
        }

        // GET: DrinkEnthusiasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkEnthusiast = await _context.DrinkEnthusiasts.FindAsync(id);
            if (drinkEnthusiast == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
            return View(drinkEnthusiast);
        }

        // POST: DrinkEnthusiasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrinkEnthusiastId,FirstName,LastName,Address,City,State,Zipcode,CheckInStatus,ApplicationId")] DrinkEnthusiast drinkEnthusiast)
        {
            if (id != drinkEnthusiast.DrinkEnthusiastId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkEnthusiast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkEnthusiastExists(drinkEnthusiast.DrinkEnthusiastId))
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
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
            return View(drinkEnthusiast);
        }

        // GET: DrinkEnthusiasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkEnthusiast = await _context.DrinkEnthusiasts
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DrinkEnthusiastId == id);
            if (drinkEnthusiast == null)
            {
                return NotFound();
            }

            return View(drinkEnthusiast);
        }

        // POST: DrinkEnthusiasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinkEnthusiast = await _context.DrinkEnthusiasts.FindAsync(id);
            _context.DrinkEnthusiasts.Remove(drinkEnthusiast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkEnthusiastExists(int id)
        {
            return _context.DrinkEnthusiasts.Any(e => e.DrinkEnthusiastId == id);
        }
    }
}
