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
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IpmsUser> _userManager;

        public SuppliersController(ApplicationDbContext context, UserManager<IpmsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(SearchViewModel searchViewModel)
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);

            var query = _context.Suppliers.Where(s => s.IpmsUserId == ipmsUser.Id).AsQueryable();
            if (!String.IsNullOrWhiteSpace(searchViewModel.SearchSupplier))
            {
                query = query.Where(s => s.Name.Contains(searchViewModel.SearchSupplier));
            }


            searchViewModel.SResult = await query.ToListAsync();
            return View(searchViewModel);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IpmsUser")] Supplier supplier)
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);
            supplier.IpmsUserId = ipmsUser.Id;

            if (ModelState.IsValid)
            {
                
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (supplier.IpmsUserId != currentUser.Id)
            {
                return Unauthorized();
            }
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldSupplier = await _context.Suppliers.FindAsync(id);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    if (oldSupplier.IpmsUserId != currentUser.Id)
                    {
                        return Unauthorized();
                    }

                    oldSupplier.Description = supplier.Description;
                    oldSupplier.Name = supplier.Name;
                    _context.Update(oldSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
