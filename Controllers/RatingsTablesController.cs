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
    public class RatingsTablesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RatingsTablesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: RatingsTables
        public async Task<IActionResult> Index()
        {
            return View(await _db.RatingsTables.ToListAsync());
        }

        // GET: RatingsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingsTable = await _db.RatingsTables
                .FirstOrDefaultAsync(m => m.RatingsTableId == id);
            if (ratingsTable == null)
            {
                return NotFound();
            }

            return View(ratingsTable);
        }

        // GET: RatingsTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RatingsTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingsTableId,BarOwnerId,DrinkEnthusiastId,CustomerRating")] RatingsTable ratingsTable)
        {
            if (ModelState.IsValid)
            {
                _db.Add(ratingsTable);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ratingsTable);
        }

        // GET: RatingsTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingsTable = await _db.RatingsTables.FindAsync(id);
            if (ratingsTable == null)
            {
                return NotFound();
            }
            return View(ratingsTable);
        }

        // POST: RatingsTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingsTableId,BarOwnerId,DrinkEnthusiastId,CustomerRating")] RatingsTable ratingsTable)
        {
            if (id != ratingsTable.RatingsTableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(ratingsTable);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingsTableExists(ratingsTable.RatingsTableId))
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
            return View(ratingsTable);
        }

        // GET: RatingsTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingsTable = await _db.RatingsTables
                .FirstOrDefaultAsync(m => m.RatingsTableId == id);
            if (ratingsTable == null)
            {
                return NotFound();
            }

            return View(ratingsTable);
        }

        // POST: RatingsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingsTable = await _db.RatingsTables.FindAsync(id);
            _db.RatingsTables.Remove(ratingsTable);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingsTableExists(int id)
        {
            return _db.RatingsTables.Any(e => e.RatingsTableId == id);
        }
    }
}
