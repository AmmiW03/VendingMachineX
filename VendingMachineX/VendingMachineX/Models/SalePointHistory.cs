using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineX.Models
{
    public class SalePointHistory
    {
        public String Id {  get; set; } 
        public List<ProductSale> SalePoints = new List<ProductSale>();
    }

    public class ProductSale
    {
        public DateTime Date {  get; set; }
        public float Quantity { get; set; }
    }
}
