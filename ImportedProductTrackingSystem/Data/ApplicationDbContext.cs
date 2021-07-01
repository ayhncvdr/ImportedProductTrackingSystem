using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ImportedProductTrackingSystem.Models;

namespace ImportedProductTrackingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IpmsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
