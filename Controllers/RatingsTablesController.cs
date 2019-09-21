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
        private readonly ApplicationDbContext _context;

        public RatingsTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RatingsTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.RatingsTables.ToListAsync());
        }

        // GET: RatingsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingsTable = await _context.RatingsTables
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
                _context.Add(ratingsTable);
                await _context.SaveChangesAsync();
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

            var ratingsTable = await _context.RatingsTables.FindAsync(id);
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
                    _context.Update(ratingsTable);
                    await _context.SaveChangesAsync();
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

            var ratingsTable = await _context.RatingsTables
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
            var ratingsTable = await _context.RatingsTables.FindAsync(id);
            _context.RatingsTables.Remove(ratingsTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingsTableExists(int id)
        {
            return _context.RatingsTables.Any(e => e.RatingsTableId == id);
        }
    }
}
