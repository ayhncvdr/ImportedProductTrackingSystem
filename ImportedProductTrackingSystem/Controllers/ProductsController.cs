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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IpmsUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<IpmsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(SearchViewModel searchViewModel,bool orderbyPrice=false, bool orderbyCountry = false, bool orderbySupplier = false, bool orderbyName = false, bool orderbyCustomOffice=false)
        {
            var IpmsUser = await _userManager.GetUserAsync(HttpContext.User);

            var query = _context.Products.Include(p => p.Country).Include(p => p.Supplier).Where(p=> p.IpmsUserId==IpmsUser.Id).AsQueryable();
            if (orderbyPrice==true)
            {
                query= query.OrderByDescending(p => p.GoodsValue);
                
            }
            else if (orderbyCountry == true)
            {
                query = query.OrderBy(p => p.Country);

            }
            else if (orderbySupplier == true)
            {
                query = query.OrderBy(p => p.Supplier);

            }
            else if (orderbyName == true)
            {
                query = query.OrderBy(p => p.Name);

            }
            else if (orderbyCustomOffice == true)
            {
                query = query.OrderByDescending(p => p.CustomOffice);

            }

            else if (!String.IsNullOrWhiteSpace(searchViewModel.SearchProduct))
            {
                query = query.Where(p => p.Name.Contains(searchViewModel.SearchProduct));
            }

            else
            {
                query = query.OrderByDescending(p => p.InvoiceDate);
            }
            
            
               
           // applicationDbContext = applicationDbContext.OrderByDescending(p => p.ValueAddedTaxPaidToCustoms);

           searchViewModel.Result = await query.ToListAsync();
            return View(searchViewModel);
        }
        public async Task<IActionResult> Index2(SearchViewModel searchViewModel, bool orderbyPrice = false, bool orderbyCountry = false, bool orderbySupplier = false, bool orderbyName = false, bool orderbyCustomOffice = false)
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);

            var query = _context.Products.Include(p => p.Country).Include(p => p.Supplier).Where(p => p.IpmsUserId == ipmsUser.Id).AsQueryable();
            if (orderbyPrice == true)
            {
                query = query.OrderByDescending(p => p.GoodsValue);

            }
            else if (orderbyCountry == true)
            {
                query = query.OrderBy(p => p.Country);

            }
            else if (orderbySupplier == true)
            {
                query = query.OrderBy(p => p.Supplier);

            }
            if (!searchViewModel.SupplierId.Equals(_context.Products.Include(p =>p.SupplierId)))
            {
                query = query.Where(p => p.SupplierId == searchViewModel.SupplierId);
            }
            else if (orderbyName == true)
            {
                query = query.OrderBy(p => p.Name);

            }
            else if (orderbyCustomOffice == true)
            {
                query = query.OrderByDescending(p => p.CustomOffice);

            }

            else if (!String.IsNullOrWhiteSpace(searchViewModel.SearchProduct))
            {
                query = query.Where(p => p.Name.Contains(searchViewModel.SearchProduct));
            }

            else
            {
                query = query.OrderByDescending(p => p.InvoiceDate);
            }



            // applicationDbContext = applicationDbContext.OrderByDescending(p => p.ValueAddedTaxPaidToCustoms);

            searchViewModel.Result = await query.ToListAsync();
            return View(searchViewModel);
        }
        public async Task<IActionResult> Index3(SearchViewModel searchViewModel, bool orderbyPrice = false, bool orderbyCountry = false, bool orderbySupplier = false, bool orderbyName = false, bool orderbyCustomOffice = false)
        {
            var IpmsUser = await _userManager.GetUserAsync(HttpContext.User);
            var query = _context.Products.Include(p => p.Country).Include(p => p.Supplier).Where(p => p.IpmsUserId == IpmsUser.Id).AsQueryable();
            if (orderbyPrice == true)
            {
                query = query.OrderByDescending(p => p.GoodsValue);

            }
            else if (orderbyCountry == true)
            {
                query = query.OrderBy(p => p.Country);

            }
            else if (orderbySupplier == true)
            {
                query = query.OrderBy(p => p.Supplier);

            }
            if (!searchViewModel.CountryId.Equals(_context.Products.Include(p => p.CountryId)))
            {
                query = query.Where(p => p.CountryId == searchViewModel.CountryId);
            }
            else if (orderbyName == true)
            {
                query = query.OrderBy(p => p.Name);

            }
            else if (orderbyCustomOffice == true)
            {
                query = query.OrderByDescending(p => p.CustomOffice);

            }

            else if (!String.IsNullOrWhiteSpace(searchViewModel.SearchProduct))
            {
                query = query.Where(p => p.Name.Contains(searchViewModel.SearchProduct));
            }

            else
            {
                query = query.OrderByDescending(p => p.InvoiceDate);
            }



            // applicationDbContext = applicationDbContext.OrderByDescending(p => p.ValueAddedTaxPaidToCustoms);

            searchViewModel.Result = await query.ToListAsync();
            return View(searchViewModel);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Country)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,SupplierId,CountryId,CustomOffice,InvoiceDate,GoodsValue,CustomsDutyRate,VATRate, IpmsUserId")] Product product)
        {
            product = await _context.Products.FindAsync();
            var IpmsUser = await _userManager.GetUserAsync(HttpContext.User);
            product.IpmsUserId = IpmsUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", product.CountryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (product.IpmsUserId != currentUser.Id)
            {
                return Unauthorized();
            }
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", product.CountryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SupplierId,CountryId,CustomOffice,InvoiceDate,GoodsValue,CustomsDutyRate,VATRate,IpmsUserId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldProduct = await _context.Products.FindAsync(id);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    if (oldProduct.IpmsUserId!= currentUser.Id)
                    {
                        return Unauthorized();
                    }

                    oldProduct.InvoiceDate = product.InvoiceDate;
                    oldProduct.Country = product.Country;
                    oldProduct.CustomOffice = product.CustomOffice;
                    oldProduct.GoodsValue = product.GoodsValue;
                    oldProduct.Name = product.Name;
                    oldProduct.Supplier = product.Supplier;
                    oldProduct.CustomsDutyRate = product.CustomsDutyRate;
                    oldProduct.VATRate = product.VATRate;

                    _context.Update(oldProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", product.CountryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Country)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
