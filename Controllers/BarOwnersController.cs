using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly ApplicationDbContext _db;
        DrinkEnthusiast drinkEnthusiast;
        BarOwner barOwner;

        public BarOwnersController(ApplicationDbContext db)
        {
            _db = db;
            drinkEnthusiast = new DrinkEnthusiast();
        }

        // GET: BarOwners
        public async Task <IActionResult> Index()
        {
            //var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            //var applicationDbContext = _db.BarOwners
            //    .Include(b => b.ApplicationUser)
            //    .Where(b => b.BarOwnerId == b.BarOwnerId)
            //    .SingleOrDefaultAsync();

            //var currentBarOwner = _db.Users.Find(currentUserId);
            //return View(currentBarOwner);

            return View(await _db.BarOwners.ToListAsync());

        }

        // GET: BarOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barOwner = await _db.BarOwners
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
            return View();
        }

        // POST: BarOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BarOwnerId,BarName,Address,City,State,Zipcode,TypeOfBar,BarOpen,BarClose")] BarOwner barOwner)
        {
            if (ModelState.IsValid)
            {
                _db.Add(barOwner);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", barOwner.ApplicationId);
            return View(barOwner);
        }

        // GET: BarOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barOwner = await _db.BarOwners.FindAsync(id);
            if (barOwner == null)
            {
                return NotFound();
            }
            //ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", barOwner.ApplicationId);
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
                    _db.Update(barOwner);
                    await _db.SaveChangesAsync();
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
            //ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", barOwner.ApplicationId);
            return View(barOwner);
        }

        // GET: BarOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barOwner = await _db.BarOwners
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
            var barOwner = await _db.BarOwners.FindAsync(id);
            _db.BarOwners.Remove(barOwner);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarOwnerExists(int id)
        {
            return _db.BarOwners.Any(e => e.BarOwnerId == id);
        }

        //public async Task<IActionResult> GetStatistics()
        //{
        //    PossibleCustomers(drinkEnthusiast);
        //}

        public ActionResult PossibleCustomers(DrinkEnthusiast drinkEnthusiast) //when people click the the check in box, the potential customers property in bar model will increment
        {//the method will be a bool, if checked = true else = false, if true add, else leave it alone
            ViewBag.CheckInStatus = drinkEnthusiast.CheckInStatus;
            if (drinkEnthusiast.CheckInStatus == true)
            {
                IncrementPossibleCustomers(barOwner);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }

        public void IncrementPossibleCustomers(BarOwner barOwner)
        {
            barOwner.PotentialCustomers++;
            _db.SaveChanges();
        }
    }
}
