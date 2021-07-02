using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportedProductTrackingSystem.Data;
using ImportedProductTrackingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImportedProductTrackingSystem.ViewComponents
{
    public class CountryMenuViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IpmsUser> _userManager;
        public CountryMenuViewComponent(ApplicationDbContext dbContext, UserManager<IpmsUser> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ipmsUser = await _userManager.GetUserAsync(HttpContext.User);
            var query = dbContext.Countries.FromSqlRaw("select * from Countries").Where(c => c.IpmsUserId == ipmsUser.Id);

            var items = await query.ToListAsync();
            return View(items);
        }
    }
}
