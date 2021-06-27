using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ImportedProductTrackingSystem.Models
{
    public class Product
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name for the product")]
        [MaxLength(200)]
        public string Name { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Required(ErrorMessage = "Please enter a custom office for the product")]
        [MaxLength(200)]
        [Display(Name = "Custom Office") ]
        public string CustomOffice { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter an invoice date")]
        [Display(Name="Invoice Date")]
        public DateTime InvoiceDate { get; set; }


        [Required(ErrorMessage = "Please enter the value of the product")]
        [Display(Name = "Goods Value(TL)")]
        public float GoodsValue { get; set; }


        [Required(ErrorMessage = "Please enter a custom duty rate for the product")]
        [Range(0,100)]
        [Display(Name = "Custom Duty Rate(%)")]
        public int CustomsDutyRate { get; set; }

        [Required(ErrorMessage = "Please enter a VAT rate for the product")]
        [Range(0, 100)]
        [Display(Name = "VAT Rate(%)")]
        public int VATRate { get; set; }


        [Display(Name = "Customs Duty Paid To Customs(TL)")]
        public float CustomsDutyPaidToCustoms
        {
            get
            {
                float customsDutyPaidToCustoms = (GoodsValue * CustomsDutyRate / 100);
                return customsDutyPaidToCustoms;
            }
        }

        [Display(Name = "VAT Base(TL)")]
        public float VATBase
        {
            get
            {
                float vatBase = (GoodsValue + CustomsDutyPaidToCustoms);
                return vatBase;
            }

        }

        [Display(Name = "Value Added Tax Paid To Customs(TL)")]
        public float ValueAddedTaxPaidToCustoms
        {
            get
            {
                float valueAddedTaxPaidToCustoms = (VATBase * VATRate / 100);
                return valueAddedTaxPaidToCustoms;
            }

        }

        [Display(Name = "Total Import Value(TL)")]
        public float TotalImportValue
        {
            get
            {
                float totalImportValue = (ValueAddedTaxPaidToCustoms + VATBase);
                return totalImportValue;
            }

        }

        

       

    }
}
