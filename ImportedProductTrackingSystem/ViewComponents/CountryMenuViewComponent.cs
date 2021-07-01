using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportedProductTrackingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImportedProductTrackingSystem.ViewComponents
{
    public class CountryMenuViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        public CountryMenuViewComponent(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool ShowEmpty = true)
        {
            var items = await dbContext.Countries.ToListAsync();
            return View(items);
        }
    }
}
