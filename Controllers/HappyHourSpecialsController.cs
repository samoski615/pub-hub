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
    public class HappyHourSpecialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HappyHourSpecialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HappyHourSpecials
        public async Task<IActionResult> Index()
        {
            return View(await _context.HappyHourSpecials.ToListAsync());
        }

        // GET: HappyHourSpecials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var happyHourSpecials = await _context.HappyHourSpecials
                .FirstOrDefaultAsync(m => m.HappyHourSpecialsId == id);
            if (happyHourSpecials == null)
            {
                return NotFound();
            }

            return View(happyHourSpecials);
        }

        // GET: HappyHourSpecials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HappyHourSpecials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HappyHourSpecialsId,DayOfWeek,TypeOfDrink,DrinkPrice,HappyHourStartTime,HappyHourEndTime")] HappyHourSpecials happyHourSpecials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(happyHourSpecials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(happyHourSpecials);
        }

        // GET: HappyHourSpecials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var happyHourSpecials = await _context.HappyHourSpecials.FindAsync(id);
            if (happyHourSpecials == null)
            {
                return NotFound();
            }
            return View(happyHourSpecials);
        }

        // POST: HappyHourSpecials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HappyHourSpecialsId,DayOfWeek,TypeOfDrink,DrinkPrice,HappyHourStartTime,HappyHourEndTime")] HappyHourSpecials happyHourSpecials)
        {
            if (id != happyHourSpecials.HappyHourSpecialsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(happyHourSpecials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HappyHourSpecialsExists(happyHourSpecials.HappyHourSpecialsId))
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
            return View(happyHourSpecials);
        }

        // GET: HappyHourSpecials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var happyHourSpecials = await _context.HappyHourSpecials
                .FirstOrDefaultAsync(m => m.HappyHourSpecialsId == id);
            if (happyHourSpecials == null)
            {
                return NotFound();
            }

            return View(happyHourSpecials);
        }

        // POST: HappyHourSpecials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var happyHourSpecials = await _context.HappyHourSpecials.FindAsync(id);
            _context.HappyHourSpecials.Remove(happyHourSpecials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HappyHourSpecialsExists(int id)
        {
            return _context.HappyHourSpecials.Any(e => e.HappyHourSpecialsId == id);
        }
    }
}
