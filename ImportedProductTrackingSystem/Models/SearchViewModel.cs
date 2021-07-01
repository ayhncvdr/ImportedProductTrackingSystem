using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportedProductTrackingSystem.Models
{
    public class SearchViewModel
    {
        public string SearchProduct { get; set; }
        public bool orderbyPrice { get; set; }
        public bool orderbyCountry { get; set; }
        public bool orderbySupplier { get; set; }
        public bool orderbyName { get; set; }
        public bool orderbyCustomOffice { get; set; }

        public int SupplierId { get; set; }
        public int CountryId { get; set; }
        public List<Product> Result { get; set; }
    }
}
