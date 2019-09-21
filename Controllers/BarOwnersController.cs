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
    public class BarOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BarOwners
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BarOwners.Include(b => b.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BarOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barOwner = await _context.BarOwners
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.BarOwnerId == id);
            if (barOwner == null)
            {
                return NotFound();
            }

            return View(barOwner);
        }

        // GET: BarOwners/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: BarOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BarOwnerId,HappyHourSpecialsId,BarName,Address,City,State,Zipcode,TypeOfBar,AverageRating,BarOpen,BarClose,HappyHourStartTime,HappyHourEndTime,PotentialCustomers,ApplicationId")] BarOwner barOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", barOwner.ApplicationId);
            return View(barOwner);
        }

        // GET: BarOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barOwner = await _context.BarOwners.FindAsync(id);
            if (barOwner == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", barOwner.ApplicationId);
            return View(barOwner);
        }

        // POST: BarOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BarOwnerId,HappyHourSpecialsId,BarName,Address,City,State,Zipcode,TypeOfBar,AverageRating,BarOpen,BarClose,HappyHourStartTime,HappyHourEndTime,PotentialCustomers,ApplicationId")] BarOwner barOwner)
        {
            if (id != barOwner.BarOwnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarOwnerExists(barOwner.BarOwnerId))
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
            ViewData["ApplicationId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", barOwner.ApplicationId);
            return View(barOwner);
        }

        // GET: BarOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barOwner = await _context.BarOwners
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.BarOwnerId == id);
            if (barOwner == null)
            {
                return NotFound();
            }

            return View(barOwner);
        }

        // POST: BarOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barOwner = await _context.BarOwners.FindAsync(id);
            _context.BarOwners.Remove(barOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarOwnerExists(int id)
        {
            return _context.BarOwners.Any(e => e.BarOwnerId == id);
        }
    }
}
