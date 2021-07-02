using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImportedProductTrackingSystem.Data;
using ImportedProductTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ImportedProductTrackingSystem.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IpmsUser> _userManager;

        public CountriesController(ApplicationDbContext context, UserManager<IpmsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Countries
        public async Task<IActionResult> Index(SearchViewModel searchViewModel)
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);

            var query = _context.Countries.Where(c=>c.IpmsUserId==ipmsUser.Id).AsQueryable();
            if (!String.IsNullOrWhiteSpace(searchViewModel.SearchCountry))
            {
                query = query.Where(c => c.Name.Contains(searchViewModel.SearchCountry));
            }

            searchViewModel.CResult = await query.ToListAsync();
            return View(searchViewModel);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IpmsUser")] Country country)
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);
            country.IpmsUserId = ipmsUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (country.IpmsUserId != currentUser.Id)
            {
                return Unauthorized();
            }
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldCountry = await _context.Countries.FindAsync(id);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    if (oldCountry.IpmsUserId != currentUser.Id)
                    {
                        return Unauthorized();
                    }

                    oldCountry.Description = oldCountry.Description;
                    oldCountry.Name = oldCountry.Name;
                    _context.Update(oldCountry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
