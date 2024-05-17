using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineX.Models
{
    public class Product
    {
        public float Quantity {  get; set; }
        public String Name { get; set; }
        public int Position { get; set; }
        public int Price { get; set; }
    }
}
