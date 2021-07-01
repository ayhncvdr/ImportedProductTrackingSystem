using ImportedProductTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ImportedProductTrackingSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImportedProductTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IpmsUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<IpmsUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> result;
            if (User.Identity.IsAuthenticated)
            {
             var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);
                        var query=  _dbContext.Products.Include(p => p.Country).Include(p => p.Supplier).Where(p=>p.IpmsUserId == ipmsUser.Id).OrderByDescending(p => p.InvoiceDate).Take(5);
                        result=await query.ToListAsync();
            }
            else
            {
                result = new List<Product>();
            }
           
            return View(result);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
