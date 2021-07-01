using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ImportedProductTrackingSystem.Models
{
    public class IpmsUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<Supplier> Suppliers { get; set; }

        public virtual List<Country> Countries { get; set; }
    }
}
