using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportedProductTrackingSystem.Data;
using ImportedProductTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImportedProductTrackingSystem.ViewComponents
{
    public class SupplierMenuViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IpmsUser> _userManager;
        public SupplierMenuViewComponent(ApplicationDbContext dbContext, UserManager<IpmsUser> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool ShowEmpty=true)
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);
            var query = dbContext.Suppliers.Where(s => s.IpmsUserId == ipmsUser.Id);

            var items = await query.ToListAsync();
            return View(items);
        }
    }
}
