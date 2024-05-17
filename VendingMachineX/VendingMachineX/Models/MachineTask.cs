using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineX.Models
{
    public class MachineTask
    {
        public String Id { get; set; }
        public String Machine { get; set; }
        public String Product { get; set; }
        public float Quantity { get; set; } 
        public bool Status { get; set; }
    }
}
