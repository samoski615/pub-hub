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
        private readonly ApplicationDbContext _db;

        public DrinkEnthusiastsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: DrinkEnthusiasts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.DrinkEnthusiasts.Include(d => d.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DrinkEnthusiasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkEnthusiast = await _db.DrinkEnthusiasts
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
            ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id");
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
                _db.Add(drinkEnthusiast);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
            return View(drinkEnthusiast);
        }

        // GET: DrinkEnthusiasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkEnthusiast = await _db.DrinkEnthusiasts.FindAsync(id);
            if (drinkEnthusiast == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
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
                    _db.Update(drinkEnthusiast);
                    await _db.SaveChangesAsync();
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
            ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
            return View(drinkEnthusiast);
        }

        // GET: DrinkEnthusiasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkEnthusiast = await _db.DrinkEnthusiasts
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
            var drinkEnthusiast = await _db.DrinkEnthusiasts.FindAsync(id);
            _db.DrinkEnthusiasts.Remove(drinkEnthusiast);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkEnthusiastExists(int id)
        {
            return _db.DrinkEnthusiasts.Any(e => e.DrinkEnthusiastId == id);
        }
    }
}
