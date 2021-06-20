using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImportedProductTrackingSystem.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name for the country")]
        [MaxLength(100)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
