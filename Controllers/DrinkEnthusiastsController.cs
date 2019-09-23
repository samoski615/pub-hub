using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            return View();
        }

        // POST: DrinkEnthusiasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrinkEnthusiastId,FirstName,LastName,Address,City,State,Zipcode,ApplicationId")] DrinkEnthusiast drinkEnthusiast, ApplicationUser applicationUser)
        {
            applicationUser = new ApplicationUser();
            if (ModelState.IsValid)
            {
                _db.Add(drinkEnthusiast);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_db.Set<ApplicationUser>(), "Id", "Id", drinkEnthusiast.ApplicationId);
            return View(drinkEnthusiast);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApiCall([Bind("Id, FirstName, LastName, Address, Latitude, Longitude, City, State, Zipcode")] DrinkEnthusiast drinkEnthusiast)
        {
            if (ModelState.IsValid)
            {
                // api call
                //get the long and lat
                //add long and lat to drinkConsumers

                string apiCall = "https://maps.googleapis.com/maps/api/geocode/json?address=" + AddPluses(drinkEnthusiast.Address) + ",+" + AddPluses(drinkEnthusiast.City) + ",+" + AddPluses(drinkEnthusiast.State) + "&key=AIzaSyAY94W50Ro6315b5RFNT3TTUKMd3DsUrEU";
                HttpClient client = new HttpClient();
                //make a request for api call set up base address url 
                client.BaseAddress = new Uri(apiCall);
                HttpResponseMessage response = await client.GetAsync(apiCall);
                LocationInfo location = JsonConvert.DeserializeObject<LocationInfo>(await response.Content.ReadAsStringAsync());
                //drinkEnthusiast.Latitude = location.Results[0].Geometry.Location.Lat.ToString();
                //drinkEnthusiast.Longitude = location.Results[0].Geometry.Location.Lng.ToString();
                _db.Add(drinkEnthusiast);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drinkEnthusiast);
        }

        public string AddPluses(string str)
        {
            str = str.Replace(" ", "+");
            return str;
        }

        public string AddCommas(string str)
        {
            str = str.Replace(str, ",");
            return str;

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



        //GET: Customer Search by Type of Bar
        [HttpGet]
        public async Task<IActionResult> Search(string typeOfBar, string typeOfDrink, string zipCode, string searchString)
        {
            IQueryable<string> typeOfBarSearch = _db.BarOwners.OrderBy(b => b.TypeOfBar).Select(b => typeOfBar);

            var bars = _db.BarOwners.Select(b => b);

                if (!String.IsNullOrEmpty(searchString))
                {
                    bars = bars.Where(s => s.TypeOfBar.Contains(searchString));
                }
                if (!String.IsNullOrEmpty(typeOfBar))
                {
                    bars = bars.Where(s => s.TypeOfBar == typeOfBar);
                }
                //if (!String.IsNullOrEmpty(typeOfDrink))
                //{
                //    var drinkResult = bars.Where(s => s.TypeOfDrink == typeOfDrink);
                //}
                
  
                    var barSearchVM = new BarSearchViewModel
                    {
                        TypeOfBar = new SelectList(await typeOfBarSearch.Distinct().ToListAsync()),
                        BarOwners = await bars.ToListAsync()
                    };
                    return View(barSearchVM);
        }

        //POST: Customer Search by Type of Bar
        [HttpPost]
        public async Task<IActionResult> Search(string typeOfBar, string searchString, BarSearchViewModel barSearchVM)
        {
            IQueryable<string> typeOfBarSearch = _db.BarOwners.OrderBy(b => b.TypeOfBar).Select(b => typeOfBar);

            var bars = _db.BarOwners.Select(b => b);

            if (!String.IsNullOrEmpty(searchString))
            {
                bars = bars.Where(s => s.TypeOfBar.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(typeOfBar))
            {
                bars = bars.Where(s => s.TypeOfBar == typeOfBar);
            }
            //if (!String.IsNullOrEmpty(typeOfDrink))
            //{
            //    var drinkResult = bars.Where(s => s.TypeOfDrink == typeOfDrink);
            //}


            barSearchVM = new BarSearchViewModel
            {
                TypeOfBar = new SelectList(await typeOfBarSearch.Distinct().ToListAsync()),
                BarOwners = await bars.ToListAsync()
            };
            return View(barSearchVM);
        }

    }
}
